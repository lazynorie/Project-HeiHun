using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CameraHandler : MonoBehaviour
{
    private InputHandler inputHandler;
    public Transform targetTransform;
    public Transform cameraTransform;
    public Transform cameraPivotTransform;
    private Transform myTransform;
    private Vector3 cameraTransformPosition;
    public LayerMask ignoreLayer;
    private Vector3 cameraFollowVelocity = Vector3.zero;

    public static CameraHandler singleton;

    public float lookSpeed = 0.1f;
    public float followSpeed = 0.1f;
    public float pivotSpeed = 0.03f;

     
    private float defaultPosition;
    private float lookAngle;
    private float pivotAngle;
    public float minimumPivot = -35;
    public float maximumPivot = 35;

    //摄像头碰撞
    private float targetPosition;
    public float cameraSphereRadius = 0.2f;
    public float cameraCollisionOffSet = 0.2f;
    public float minimumCollisionOffSet = 0.2f;

    [Header("Lock On")] 
    [SerializeField] private float maximumLockOnDistance;
    public Transform nearestLockOnTarget;
    private List<CharacterManager> availableTargets = new List<CharacterManager>();
    public Transform currentLockOnTarget;
    public Transform leftLockTarget;
    public Transform rightLockTarget;
    [Header("Debug")]
    public GameObject currentHitObject = null;


    private void Awake()
    {
        singleton = this;
        myTransform = transform;
        defaultPosition = cameraTransform.localPosition.z;
        inputHandler = FindObjectOfType<InputHandler>();
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        ignoreLayer = ~(1 << 8 | 1 << 9 | 1 << 10);
    }

    public void FollowTarget(float delta)
    {
        //Vector3 targetPosition = Vector3.Lerp(myTransform.position, tragetTransform.position, delta / followSpeed);
        Vector3 targetPosition = Vector3.SmoothDamp
            (myTransform.position, targetTransform.position, ref cameraFollowVelocity, delta / followSpeed);

        myTransform.position = targetPosition;
        
        HandleCameraCollision(delta);
    }

    public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput)
    {
        //if there's no lock on
        if (inputHandler.lockOnFlag == false && currentLockOnTarget == null)
        {
            lookAngle += (mouseXInput * lookSpeed) / delta ;
            pivotAngle -= (mouseYInput * pivotSpeed) / delta ;
            pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

            Vector3 rotation = Vector3.zero;
            rotation.y = lookAngle;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            myTransform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = pivotAngle;

            targetRotation = Quaternion.Euler(rotation);
            cameraPivotTransform.localRotation = targetRotation;
        }
        //lock on activate
        else
        {
            float velocity = 0;

            Vector3 dir = currentLockOnTarget.position - transform.position;
            dir.Normalize();
            dir.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(dir);
            //transform.rotation = targetRotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.5f);

            dir = currentLockOnTarget.position - cameraPivotTransform.position;
            dir.Normalize();

            targetRotation = Quaternion.LookRotation(dir);
            Vector3 eularAngle = targetRotation.eulerAngles;
            eularAngle.y = 0;
            float xangle = Mathf.LerpAngle(cameraPivotTransform.localEulerAngles.x, eularAngle.x, 0.5f);
            float zangle = Mathf.LerpAngle(cameraPivotTransform.localEulerAngles.z, eularAngle.z, 0.5f);
            cameraPivotTransform.localEulerAngles = new Vector3(xangle, 0, zangle);
            //cameraPivotTransform.localEulerAngles = eularAngle;

        }
    }

    private void HandleCameraCollision(float delta)
    {
        targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
        direction.Normalize();
        
        if (Physics.SphereCast
                (cameraPivotTransform.position,cameraSphereRadius,direction,out hit, 
                    Mathf.Abs(targetPosition),ignoreLayer))
        {
            float dis = Vector3.Distance(cameraPivotTransform.position, hit.point);
            targetPosition = -(dis - cameraCollisionOffSet);
            
            currentHitObject = hit.transform.gameObject;
            //print(hit.transform.name);
            //Debug.DrawRay(cameraPivotTransform.position,direction* dis,Color.blue);
        }
        else
        {
            currentHitObject = null;
        }
        
        if (Mathf.Abs(targetPosition)<minimumCollisionOffSet)
        {
            targetPosition = -minimumCollisionOffSet;
        }
        
        cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, delta / 0.2f);
        cameraTransform.localPosition = cameraTransformPosition;

    }
    //Lock on method
    public void HandleLockOn()
    {
        float shortestDistance = Mathf.Infinity;
        float shortestDistanceOfLeftTarget = Mathf.Infinity;
        float shortestDistanceOfRightTarget = Mathf.Infinity;

        Collider[] colliders = Physics.OverlapSphere(targetTransform.position, 26);
        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterManager characters = colliders[i].GetComponent<CharacterManager>();
            if (characters != null)
            {
                Vector3 lockTargetDirection = characters.transform.position - targetTransform.position;
                float distanceFromTarget = Vector3.Distance(targetTransform.position, characters.transform.position);
                float viewableAngle = Vector3.Angle(lockTargetDirection, cameraTransform.forward);

                //do this to prevent camera lock on to player themselves
                if (characters.transform.root != targetTransform.transform.root 
                    && viewableAngle >-50 && viewableAngle <50 
                    && distanceFromTarget <= maximumLockOnDistance)
                {
                    availableTargets.Add(characters);
                }
            }
        }

        for (int k = 0; k < availableTargets.Count; k++)
        {
            float distanceFromTarget =
                Vector3.Distance(targetTransform.position, availableTargets[k].transform.position);

            if (distanceFromTarget < shortestDistance)
            {
                shortestDistance = distanceFromTarget;
                nearestLockOnTarget = availableTargets[k].lockOnTransform;
            }

            if (inputHandler.lockOnFlag)
            {
                //checking the how close the target is to the player base on relative x-axis
                Vector3 relativeEnemyPosition =
                    currentLockOnTarget.InverseTransformPoint(availableTargets[k].transform.position);
                var distanceFromLeftTarget =
                    currentLockOnTarget.transform.position.x - availableTargets[k].transform.position.x;
                var distanceFromRightTarget =
                    currentLockOnTarget.transform.position.x + availableTargets[k].transform.position.x;
                if (relativeEnemyPosition.x > 0.00 && distanceFromTarget < shortestDistanceOfLeftTarget)
                {
                    shortestDistanceOfLeftTarget = distanceFromLeftTarget;
                    leftLockTarget = availableTargets[k].lockOnTransform;
                }
                if (relativeEnemyPosition.x < 0.00 && distanceFromTarget < shortestDistanceOfRightTarget)
                {
                    shortestDistanceOfRightTarget = distanceFromRightTarget;
                    rightLockTarget = availableTargets[k].lockOnTransform;
                }
            }
        }
    }

    public void ClearLockOnTargets()
    {
        availableTargets.Clear();
        nearestLockOnTarget = null;
        currentLockOnTarget = null;
    }

}

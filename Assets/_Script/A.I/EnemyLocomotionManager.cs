using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLocomotionManager : MonoBehaviour
{
    private EnemyManager enemyManager;
    private EnemyAnimationHandler enemyAnimationHandler;
    private NavMeshAgent navMeshAgent;
    public Rigidbody enemyRb;

    public float distanceFromTarget;
    public float stoppingDistance = 1f;
    [SerializeField]
    public CharacterStats currentTarget;
    [SerializeField] private LayerMask detectionLayer;
    [SerializeField] private float rotationSpeed = 25f;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyAnimationHandler = GetComponentInChildren<EnemyAnimationHandler>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        enemyRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        navMeshAgent.enabled = false;
        enemyRb.isKinematic = false;
    }

    private void Update()
    {
        HandleDetection();
        GetCurrentDistantFromTarget();
    }

    public void HandleDetection()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();
            if (characterStats != null)
            {
                //todo: check team ID
                Vector3 relativeDir = transform.TransformDirection(characterStats.transform.position);
                Vector3 targetDirection = characterStats.transform.position - transform.position;
                float viewbleAngle = Vector3.Angle(targetDirection, transform.forward);
                if (viewbleAngle >enemyManager.minimumDetectionAngle && viewbleAngle <enemyManager.maximumDetectionAngle)
                {
                    currentTarget = characterStats;
                }
                /*else     //save for state machine
                {
                    currentTarget = null;
                }*/
            }
        }
    }

    public void HandleMoveToTarget()
    {
        if (enemyManager.isPerformingAction) return;
        Vector3 targetDir = currentTarget.transform.position - transform.position;
        distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
        float viewableAngle = Vector3.Angle(targetDir, transform.forward);

        if (enemyManager.isPerformingAction)
        {
            enemyAnimationHandler.animator.SetFloat("Vertical",0 ,0.1f,Time.deltaTime);
            navMeshAgent.enabled = false;
        }
        else
        {
            if (distanceFromTarget > stoppingDistance)
            {
                enemyAnimationHandler.animator.SetFloat("Vertical",1.5f,0.1f,Time.deltaTime);
            }
            else if (distanceFromTarget <= stoppingDistance)
            {
                enemyAnimationHandler.animator.SetFloat("Vertical",0,0.1f,Time.deltaTime);

            }
        }

        HandleRotateTowardsTarget();//call this before navmesh location and rotation reset
        ResetNavmeshLocationRotation();
    }

    private void HandleRotateTowardsTarget()
    {
        if (enemyManager.isPerformingAction)//rotate manually
        {
            Vector3 direction = currentTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();
            if (direction == Vector3.zero)
            {
                direction = transform.forward;
            }
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation =
                Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed/Time.deltaTime);
        }
        else//rotate with pathfinding
        {
            Vector3 relativeDir = transform.InverseTransformDirection(navMeshAgent.desiredVelocity);
            Vector3 targetVelocity = enemyRb.velocity;

            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(currentTarget.transform.position);
            enemyRb.velocity = targetVelocity;
            transform.rotation = Quaternion.Slerp(transform.rotation, navMeshAgent.transform.rotation,
                rotationSpeed / Time.deltaTime);
        }
    }

    private void ResetNavmeshLocationRotation()
    {
        navMeshAgent.transform.localPosition = Vector3.zero;
        navMeshAgent.transform.localRotation = Quaternion.identity;
    }

    private void GetCurrentDistantFromTarget()
    {
        if (currentTarget != null)
        {
            distanceFromTarget = Vector3.Distance(currentTarget.transform.position ,transform.position);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerLocalmotion : MonoBehaviour
{
   Transform cameraObject;
   InputHandler inputHandler;
   Vector3 moveDirection;
   private PlayerManager playerManager;
   
   [HideInInspector] 
   public Transform myTransform;

   [HideInInspector] public AnimationHandler animhandler;
   
   public new Rigidbody rigidbody;
   public GameObject normalCamera;

   [Header("Stats")] 
   [SerializeField] private float movementSpeed = 5;
   [SerializeField] private float sprintSpeed = 7;
   [SerializeField] private float rotationSpeed = 10;
   void Start()
   {
      playerManager = GetComponent<PlayerManager>();
      rigidbody = GetComponent<Rigidbody>();
      inputHandler = GetComponent<InputHandler>();
      animhandler = GetComponentInChildren<AnimationHandler>();
      cameraObject = Camera.main.transform;
      myTransform = transform;
      animhandler.Initialize();
   }
   #region Movement
   Vector3 normalVector;
   Vector3 targetPosition;
   
   private void HandleRotation(float delta)
   {
      Vector3 targetDir = Vector3.zero;
      float moveOverrider = inputHandler.moveAmount;

      targetDir = cameraObject.forward * inputHandler.vertical;
      targetDir += cameraObject.right * inputHandler.horizontal;
      
      targetDir.Normalize();
      targetDir.y = 0;

      if (targetDir == Vector3.zero)
      {
         targetDir = myTransform.forward;
      }

      float rs = rotationSpeed;

      Quaternion tr = Quaternion.LookRotation(targetDir);
      Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

      myTransform.rotation = targetRotation;
   }
   public void HandleMovement(float delta)
   {
      if (inputHandler.rollFlag)
         return;
      
      moveDirection = cameraObject.forward * inputHandler.vertical;
      moveDirection += cameraObject.right * inputHandler.horizontal;
      moveDirection.Normalize();
      //???????????????Y???????????????
      moveDirection.y = 0;
      

      float speed = movementSpeed;

      if (inputHandler.sprintFlag)
      {
         speed = sprintSpeed;
         playerManager.isSprinting = true;
         moveDirection *= speed;
      }
      else
      {
         moveDirection *= speed;
      }
      

      Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
      rigidbody.velocity = projectedVelocity;
      
      animhandler.UpdateAnimatorValues(inputHandler.moveAmount, 0, playerManager.isSprinting);

      if (animhandler.canRotate)
      {
         HandleRotation(delta);
      }
   }
   public void HandleRollingAndSprinting(float delta)
   {
      if (animhandler.animator.GetBool("isInteracting"))
         return;

      if (inputHandler.rollFlag)
      {
         moveDirection = cameraObject.forward * inputHandler.vertical;
         moveDirection += cameraObject.right * inputHandler.horizontal;

         if (inputHandler.moveAmount>0)
         {
            animhandler.PlayTargetAnimation("StandToRoll",true);
            moveDirection.y = 0;
            Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
            myTransform.rotation = rollRotation;
         }
         else
         {
            animhandler.PlayTargetAnimation("BackStep",true);
         }
      }
   }
   #endregion
}

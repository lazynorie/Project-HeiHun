using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerLocalmotion : MonoBehaviour
{
   Transform cameraObject;
   InputHandler inputHandler;
   Vector3 moveDirection;

   [HideInInspector] 
   public Transform myTransform;

   public new Rigidbody rigidbody;
   public GameObject normalCamera;

   [Header("Stats")] 
   [SerializeField] private float movementSpeed = 5;
   [SerializeField] private float rotationSpeed = 10;
      
   void Start()
   {
      rigidbody = GetComponent<Rigidbody>();
      inputHandler = GetComponent<InputHandler>();
      cameraObject = Camera.main.transform;
      myTransform = transform;
   }

   public void Update()
   {
      float delta = Time.deltaTime;
      
      inputHandler.TickInput(delta);

      moveDirection = cameraObject.forward * inputHandler.vertical;
      moveDirection += cameraObject.right * inputHandler.horizontal;
      moveDirection.Normalize();

      float speed = movementSpeed;
      moveDirection *= speed;

      Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
      rigidbody.velocity = projectedVelocity;
      
      
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

   #endregion
}

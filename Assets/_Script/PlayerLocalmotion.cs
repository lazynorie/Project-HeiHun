using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerLocalmotion : MonoBehaviour
{
   private PlayerManager playerManager;
   private PlayerStats playerStats;
   Transform cameraObject;
   InputHandler inputHandler;
   private CameraHandler cameraHandler;
   private PlayerAnimationHandler animhandler;
   
   public Vector3 moveDirection;
   
   [HideInInspector] 
   public Transform myTransform;
   public new Rigidbody rigidbody;
   public GameObject normalCamera;

   [Header("Player Collision")]
   public CapsuleCollider characterCollider;
   public CapsuleCollider collisonCollider;
   [Header("Stamina Cost")] public int sprintStaminaCost;
   
   [Header("Movement Stats")] 
   [SerializeField] private float walkingSpeed = 3;
   [SerializeField] private float movementSpeed = 5;
   [SerializeField] private float sprintSpeed = 7;
   public float rotationSpeed = 10;
   [SerializeField] private float fallingSpeed = 45;
   [SerializeField] private float strafingSpeed = 2f;

   [Header("Ground and Air Detection Stats")] 
   [SerializeField] private float groundDetectionRayStartPoint = 0.5f;
   [SerializeField] private float minimumDistanceNeededToBeginFall = 1f;
   [SerializeField] private float groundDirectionRayDistance = 0.2f;
   [SerializeField]private LayerMask ignoreForGroundCheck;
   public float inAirTimer;

   private void Awake()
   {
      cameraHandler = FindObjectOfType<CameraHandler>();
      playerManager = GetComponent<PlayerManager>();
      playerStats = GetComponent<PlayerStats>();
      rigidbody = GetComponent<Rigidbody>();
      inputHandler = GetComponent<InputHandler>();
      animhandler = GetComponentInChildren<PlayerAnimationHandler>();
   }

   void Start()
   {
      cameraObject = Camera.main.transform;
      myTransform = transform;
      animhandler.Initialize();
      playerManager.isGrounded = true;
      Physics.IgnoreCollision(characterCollider,collisonCollider,true);
   }
   #region Movement
   Vector3 normalVector;
   Vector3 targetPosition;

   public void HandleRotation()
   {
      if (animhandler.canRotate)
      {
         //HandleRotation(delta);
         if (inputHandler.lockOnFlag)//lock on
         {
            if (inputHandler.sprintFlag || inputHandler.rollFlag)
            {
               Vector3 targetDirection = Vector3.zero;
               targetDirection = cameraObject.forward * inputHandler.vertical;
               targetDirection += cameraObject.right * inputHandler.horizontal;
               targetDirection.Normalize();
               targetDirection.y = 0;
               if (targetDirection == Vector3.zero)
               {
                  targetDirection = transform.forward;
               }
         
               Quaternion tr = Quaternion.LookRotation(targetDirection);
               Quaternion targetRotation = Quaternion.Slerp(transform.rotation,tr,rotationSpeed*Time.deltaTime);

               transform.rotation = targetRotation;
            }
            else
            {
            
               Vector3 rotationDirection = moveDirection;
               rotationDirection = cameraHandler.currentLockOnTarget.transform.position - transform.position;
               rotationDirection.y = 0;
               rotationDirection.Normalize();
               Quaternion tr = Quaternion.LookRotation(rotationDirection);
               Quaternion targetRotation = Quaternion.Slerp(transform.rotation,tr, rotationSpeed * Time.deltaTime);
               transform.rotation = targetRotation;
            }
         
         }
         else//no lock on
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
            Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * Time.deltaTime);

            myTransform.rotation = targetRotation;
         }
      }
   }
   public void HandleMovement(float delta)
   {
      if(playerManager.isInteracting)
         return;
      
      moveDirection = cameraObject.forward * inputHandler.vertical;
      moveDirection += cameraObject.right * inputHandler.horizontal;
      moveDirection.Normalize();
      //限制玩家在Y轴上的移动
      moveDirection.y = 0;
      

      float speed = movementSpeed;

      if (inputHandler.sprintFlag && inputHandler.moveAmount>0.5 && playerStats.currentStamina >= 0)
      {
         speed = sprintSpeed;
         playerManager.isSprinting = true;
         moveDirection *= speed;
         playerStats.currentStamina -= delta * sprintStaminaCost;//todo :move to playStats script
      }
      else
      {
         if (inputHandler.moveAmount<0.5)
         {
            if (inputHandler.lockOnFlag)
            {
               moveDirection *= strafingSpeed;
               playerManager.isSprinting = false;
            }
            else
            {
               moveDirection *= walkingSpeed;
               playerManager.isSprinting = false;
            }
            
         }
         else
         {
            if (inputHandler.lockOnFlag)
            {
               moveDirection *= strafingSpeed;
               playerManager.isSprinting = false;
            }
            else
            {
               moveDirection *= speed;
               playerManager.isSprinting = false;
            }
         }
      }
      
      Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
      rigidbody.velocity = projectedVelocity;

      if (inputHandler.lockOnFlag && inputHandler.sprintFlag == false)
      {
         animhandler.UpdateAnimatorValues(inputHandler.vertical, inputHandler.horizontal, playerManager.isSprinting);
      }
      else
      {
         animhandler.UpdateAnimatorValues(inputHandler.moveAmount, 0, playerManager.isSprinting);
      }
   }
   public void HandleRollingAndSprinting(float delta)
   {
      if (playerStats.currentStamina <= 0) return;
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
   public void HandleFalling(float delta, Vector3 moveDirection)
   {
      playerManager.isGrounded = false;
      RaycastHit hit;
      Vector3 origin = myTransform.position;
      origin.y += groundDetectionRayStartPoint;

      if (Physics.Raycast(origin, myTransform.forward, out hit, 0.4f))
      {
         moveDirection = Vector3.zero;
      }

      if (playerManager.isInAir)
      {
         rigidbody.AddForce(Vector3.down * fallingSpeed);
         rigidbody.AddForce(moveDirection * fallingSpeed / 8f);
      }

      Vector3 dir = moveDirection;
      dir.Normalize();
      origin = origin + dir * groundDirectionRayDistance;

      targetPosition = myTransform.position;

      Debug.DrawRay(origin, Vector3.down * minimumDistanceNeededToBeginFall, Color.red, Time.deltaTime, false);
      if (Physics.Raycast(origin, Vector3.down,out hit, minimumDistanceNeededToBeginFall, ignoreForGroundCheck))
      {
         normalVector = hit.normal;
         Vector3 tp = hit.point;
         playerManager.isGrounded = true;
         targetPosition.y = tp.y;

         if (playerManager.isInAir)
         {
            if (inAirTimer> 0.5f)
            {
               Debug.Log("you are in air for "+ inAirTimer);
               animhandler.PlayTargetAnimation("Landing", true);
               inAirTimer = 0;
            }
            else
            {
               animhandler.PlayTargetAnimation("Empty",false);
               inAirTimer = 0;
            }

            playerManager.isInAir = false;
         }
      }
      else
      {
         if (playerManager.isGrounded)
         {
            playerManager.isGrounded = false;
         }

         if (playerManager.isInAir == false)
         {
            if (playerManager.isInteracting == false) 
            {
               animhandler.PlayTargetAnimation("Falling",true);
            }

            Vector3 vel = rigidbody.velocity;
            vel.Normalize();
            rigidbody.velocity = vel * (movementSpeed / 2);
            playerManager.isInAir = true;
         }
      }
      
      if (playerManager.isInteracting || inputHandler.moveAmount > 0)
      {
         myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime / 0.1f);
      }
      else
      {
         myTransform.position = targetPosition;
      }
      
      if (playerManager.isGrounded)
      {
         if (playerManager.isInteracting || inputHandler.moveAmount>0)
         {
            myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime);
            //myTransform.position = targetPosition;
         }
         else
         {
            myTransform.position = targetPosition;
         }
      }
   }
   #endregion

   public void StopPlayer()
   {
      rigidbody.velocity = Vector3.zero;
   }

   public void RotateTowardsTarget(Transform target,float rotationRate)
   {
      Vector3 targetDir = transform.root.eulerAngles;
      targetDir = target.position - transform.position;
      targetDir.y = 0;
      targetDir.Normalize();
      Quaternion tr = Quaternion.LookRotation(targetDir);
      Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rotationRate * Time.deltaTime);
      transform.rotation = targetRotation;
   }
}

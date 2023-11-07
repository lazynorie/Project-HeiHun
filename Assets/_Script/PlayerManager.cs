using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerManager : CharacterManager
{ 
    public static event Action OnInteractable; 
    private InputHandler inputHandler;
    private Animator animator;
    private PlayerAnimationHandler playerAnimationHandler;
    public CameraHandler cameraHandler;
    private PlayerLocalmotion playerLocomotion; 
    private InteractableUI interactableUI;
    public bool isInteracting;
    [Header("Player Flags")] 
    public bool isInvulnerable;
    public bool isSprinting;
    public bool isInAir;
    public bool isGrounded;
    public bool isUsingRightHand;
    public bool isUsingLeftHand;
    
    private const int TargetFPS = 120;
    protected override void Awake()
    {
        base.Awake();
        //在这里设置目标FPS
        Application.targetFrameRate = TargetFPS;
        //cameraHandler = FindObjectOfType<CameraHandler>();
        inputHandler = GetComponent<InputHandler>();
        animator = GetComponentInChildren<Animator>();
        playerAnimationHandler = GetComponentInChildren<PlayerAnimationHandler>();
        playerLocomotion = GetComponent<PlayerLocalmotion>();
        interactableUI = FindObjectOfType<InteractableUI>();
    }
    void Start()
    {
        
        cameraHandler = CameraHandler.singleton;

        //the interactableUIobject has to be set to active for this line to work, or assign the gameobject in editor.
    }
    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;
        inputHandler.FixedTickInput(delta);
        playerLocomotion.HandleMovement(delta);
        playerLocomotion.HandleRollingAndSprinting(delta);
        playerLocomotion.HandleFalling(delta,playerLocomotion.moveDirection);
        playerLocomotion.HandleRotation();
    }

    protected override void Update()
    {
        float delta = Time.deltaTime;
        inputHandler.TickInput(delta);
        isInteracting = animator.GetBool("isInteracting");
        canDoCombo = animator.GetBool("canDoCombo");
        isUsingRightHand = animator.GetBool("isUsingRightHand");
        isUsingLeftHand = animator.GetBool("isUsingLeftHand");
        isInvulnerable = animator.GetBool("isInvulnerable");
        //canRotate = animator.GetBool("canRotate");
        playerAnimationHandler.canRotate = animator.GetBool("canRotate");
        animator.SetBool("isBlocking",isBlocking);
        CheckForInteractableObject();
    }
    private void LateUpdate()
    {
        //inputHandler.sprintFlag = false;*/
        /*inputHandler.rbInput = false;
        inputHandler.rtInput = false;
        inputHandler.dPadLeft = false;
        inputHandler.dPadRight = false;
        inputHandler.dPadUp = false;
        inputHandler.dPadDown = false;*/
        
        if (isInAir)
        {
            playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;
        }
        
        float delta = Time.fixedDeltaTime;
        if (cameraHandler != null)
        {
            cameraHandler.FollowTarget(delta);
            cameraHandler.HandleCameraRotation(delta,inputHandler.mouseX,inputHandler.mouseY);
        }
    }

    #region Interactions
    public void CheckForInteractableObject(){
        RaycastHit hit;
        if(Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit, 1f, cameraHandler.ignoreLayer))
        {
            if(hit.collider.tag == "Interactable")
            {
                Interactable interactableObject = hit.collider.GetComponent<Interactable>();
                if (interactableObject != null)
                {
                    string interactableText = interactableObject.interacibleText;
                    //set UI test to display info like item names and info 
                    interactableUI.text.text = interactableText;
                    interactableUI.EnableItemPopUpFrame();
                    //itemPopUpFrame.SetActive(true);
                    if(inputHandler.aInput)
                    {
                        OnInteractable?.Invoke();
                        hit.collider.GetComponent<Interactable>().Interact(this);
                        interactableUI.DisableItemPopUpFrame();
                    }
                }
            }
        }
        else{
            if(interactableUI.itemPopUpFrame != null)
            {
                //itemPopUpFrame.SetActive(false);
                interactableUI.DisableItemPopUpFrame();
            }

            if (interactableUI.itemPickUpFrame != null  && inputHandler.aInput)
            {
                interactableUI.DisableItemPickUpFrame();
                //itemPickUpFrame.SetActive(false);
                //Unpause Game
                CustomTime.LocalTimeScale = 1.0f;
            }
        }
    }

    public void OpenChestInteraction(Transform playerTransformWhenOpenAChest)
    {
        transform.position = playerTransformWhenOpenAChest.position;
        playerAnimationHandler.PlayTargetAnimation("Open Chest", true);
    }

    public void EnterFogWallInteraction(Transform fogWallEntrance)
    {
        playerLocomotion.StopPlayer();
        playerLocomotion.RotateTowardsTarget(fogWallEntrance,500);
        playerAnimationHandler.PlayTargetAnimation("EntryFogWall",true);
        //transform.position = fogWallEntrance.position;
    }

    #endregion
}

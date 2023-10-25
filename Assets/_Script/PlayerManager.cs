using System;
using UnityEngine;

public class PlayerManager : CharacterManager
{ 
    public static event Action OnInteractable; 
    private InputHandler inputHandler;
    private Animator animator;
    public CameraHandler cameraHandler;
    private PlayerLocalmotion playerLocomotion; 
    private InteractableUI interactableUI;
    public bool isInteracting;
    [Header("Player Flags")] 
    public bool isInvulnerable;
    public bool isSprinting;
    public bool isInAir;
    public bool isGrounded;
    public bool canDoCombo;
    public bool isUsingRightHand;
    public bool isUsingLeftHand;
    
    [Header("interactable item UI elements")]
    [SerializeField] private GameObject interactableUIGameObject;

    [SerializeField] private GameObject itemInteractableGameObject;

    private const int TargetFPS = 60;

    protected override void Awake()
    {
        base.Awake();
        //在这里设置目标FPS
        Application.targetFrameRate = TargetFPS;
        cameraHandler = CameraHandler.singleton;
        inputHandler = GetComponent<InputHandler>();
        animator = GetComponentInChildren<Animator>();
        playerLocomotion = GetComponent<PlayerLocalmotion>();
        interactableUI = FindObjectOfType<InteractableUI>();
    }
    void Start()
    {
        //the interactableUIobject has to be set to active for this line to work, or assign the gameobject in editor.
    }
    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;
        inputHandler.FixedTickInput(delta);
        playerLocomotion.HandleMovement(delta);
        playerLocomotion.HandleRollingAndSprinting(delta);
        playerLocomotion.HandleFalling(delta,playerLocomotion.moveDirection);
    }
    void Update()
    {
        float delta = Time.deltaTime;
        inputHandler.TickInput(delta);
        isInteracting = animator.GetBool("isInteracting");
        canDoCombo = animator.GetBool("canDoCombo");
        isUsingRightHand = animator.GetBool("isUsingRightHand");
        isUsingLeftHand = animator.GetBool("isUsingLeftHand");
        isInvulnerable = animator.GetBool("isInvulnerable");
        CheckForInteractableObject();
    }
    private void LateUpdate()
    {
        inputHandler.rollFlag = false;
        //inputHandler.sprintFlag = false;
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
        
        float delta = Time.deltaTime;
        if (cameraHandler != null)
        {
            cameraHandler.FollowTarget(delta);
            cameraHandler.HandleCameraRotation(delta,inputHandler.mouseX,inputHandler.mouseY);
        }
    }

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
                    interactableUIGameObject.SetActive(true);
                    if(inputHandler.raTapInput)
                    {
                        OnInteractable?.Invoke();
                        hit.collider.GetComponent<Interactable>().Interact(this);
                    }
                }
            }
        }
        else{
                if(interactableUIGameObject != null)
                {
                    interactableUIGameObject.SetActive(false);
                }

                if (itemInteractableGameObject != null  && inputHandler.raTapInput)
                {
                    itemInteractableGameObject.SetActive(false);
                    //Unpause Game
                    CustomTime.LocalTimeScale = 1.0f;
                }
            }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputHandler inputHandler;
    private Animator animator;
    public CameraHandler cameraHandler;
    private PlayerLocalmotion playerLocalmotion; 
    private InteractableUI interactableUI;
    public bool isInteracting;
    [Header("Player Flags")]
    public bool isSprinting;
    public bool isInAir;
    public bool isGrounded;
    public bool canDoCombo;
    
    [Header("interactable item UI elements")]
    [SerializeField] private GameObject interactableUIGameObject;

    [SerializeField] private GameObject itemInteractableGameObject;

    private static int targetFPS = 60;
    private void Awake()
    {
        //在这里设置目标FPS
        Application.targetFrameRate = targetFPS;
    
        cameraHandler = CameraHandler.singleton;
    }
    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        animator = GetComponentInChildren<Animator>();
        playerLocalmotion = GetComponent<PlayerLocalmotion>();
        //the interactableUIobject has to be set to active for this line to work, or assign the gameobject in editor.
        interactableUI = FindObjectOfType<InteractableUI>();
    }
    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;
        //float delta = Time.deltaTime;
        if (cameraHandler != null)
        {
            cameraHandler.FollowTarget(delta);
            cameraHandler.HandleCameraRotation(delta,inputHandler.mouseX,inputHandler.mouseY);
        }
    }
    void Update()
    {
        float delta = Time.deltaTime;
        
        isInteracting = animator.GetBool("isInteracting");
        canDoCombo = animator.GetBool("canDoCombo");
        
        inputHandler.TickInput(delta);
        playerLocalmotion.HandleMovement(delta);
        playerLocalmotion.HandleRollingAndSprinting(delta);
        playerLocalmotion.HandleFalling(delta,playerLocalmotion.moveDirection);

        CheckForInteractableObject();
    }
    private void LateUpdate()
    {
        inputHandler.rollFlag = false;
        inputHandler.sprintFlag = false;
        inputHandler.rbInput = false;
        inputHandler.rtInput = false;
        //isSprinting = inputHandler.bInput;
        inputHandler.dPadLeft = false;
        inputHandler.dPadRight = false;
        inputHandler.dPadUp = false;
        inputHandler.dPadDown = false;
        
        if (isInAir)
        {
            playerLocalmotion.inAirTimer = playerLocalmotion.inAirTimer + Time.deltaTime;
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
                    if(inputHandler.raInput)
                    {
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

                if (itemInteractableGameObject != null  && inputHandler.raInput)
                {
                    itemInteractableGameObject.SetActive(false);
                    //Unpause Game
                    CustomTime.LocalTimeScale = 1.0f;
                }
            }
    }
}

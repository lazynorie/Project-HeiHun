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
    
    public bool isInteracting;
    [Header("Player Flags")]
    public bool isSprinting;
    public bool isInAir;
    public bool isGrounded;
    public bool canDoCombo;
    
    private void Awake()
    {
        //在这里设置目标FPS
        Application.targetFrameRate = 60;
    
        cameraHandler = CameraHandler.singleton;
    }
    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        animator = GetComponentInChildren<Animator>();
        playerLocalmotion = GetComponent<PlayerLocalmotion>();
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
            Interactable interactableObject = hit.collider.GetComponent<Interactable>();
            if(hit.collider.tag == "Interactable")
            {
                string interactableText = interactableObject.interacibleText;
                //set UI test to display info like item names and info 

                if(inputHandler.raInput){
                    hit.collider.GetComponent<Interactable>().Interact(this);
                }
            }
        }
    }
}

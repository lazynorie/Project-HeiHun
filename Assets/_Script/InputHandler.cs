using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
  public float horizontal;
  
  public float vertical;
  public float moveAmount;
  public float mouseX;
  public float mouseY;

  public bool bInput;
  public bool rollFlag;
  public float rollInputTimer;
  public bool isInteracting;
  public bool sprintFlag;
  
  PlayerControls inputActions;
  CameraHandler cameraHandler;

  Vector2 movementInput;
  Vector2 cameraInput;

  private void Awake()
  {
    //在这里设置目标FPS
    Application.targetFrameRate = 60;
    
    cameraHandler = CameraHandler.singleton;
  }

  private void FixedUpdate()
  {
    float delta = Time.fixedDeltaTime;
    //float delta = Time.deltaTime;
    

    if (cameraHandler != null)
    {
      cameraHandler.FollowTarget(delta);
      cameraHandler.HandleCameraRotation(delta,mouseX,mouseY);
    }
  }

  public void OnEnable()
  {
    if (inputActions == null)
    {
      inputActions = new PlayerControls();
      inputActions.PlayerMovement.Movement.performed += 
        inputActions => movementInput = inputActions.ReadValue<Vector2>();
      inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
    }
    
    inputActions.Enable();
  }

  private void OnDisable()
  {
    inputActions.Disable();
  }

  public void TickInput(float delta)
  {
    MoveInput(delta);
    HandleRollInput(delta);
  }

  private void MoveInput(float delta)
  {
    horizontal = movementInput.x;
    vertical = movementInput.y;
    moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
    mouseX = cameraInput.x;
    mouseY = cameraInput.y;
  }

  private void HandleRollInput(float delta)
  {
    //bInput = inputActions.PlayerAction.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;
    //以上代码新版inputsystem错误
    //bInput = inputActions.PlayerAction.Roll.triggered;
    bInput = inputActions.PlayerAction.Roll.IsPressed();
    if (bInput)
    {
      //rollFlag = true;
      rollInputTimer += delta;
      sprintFlag = true;
    }
    else
    {
      if (rollInputTimer>0 && rollInputTimer<0.5f)
      {
        sprintFlag = false;
        rollFlag = true;
      }
      rollInputTimer = 0;
    }
  }
}

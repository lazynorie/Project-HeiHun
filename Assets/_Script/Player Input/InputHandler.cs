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

  public bool rollFlag;
  public float rollInputTimer;
  
  public bool bInput;
  public bool rbInput;
  public bool raInput;
  public bool rtInput;
  public bool dPadUp;
  public bool dPadDown;
  public bool dPadLeft;
  public bool dPadRight;
  public bool startInput;
  
  public bool lockOnInput;
  public bool rightStickLeftInput;
  public bool rightStickRightInput;
  
  public bool lockOnFlag;
  public bool sprintFlag;
  public bool comboFlag;
  
  PlayerControls inputActions;
  CameraHandler cameraHandler;
  PlayerAttacker playerAttacker;
  private PlayerInventory playerInventory;
  private PlayerManager playerManager;
  private UIManager uiManager;

  public Vector2 movementInput;
  Vector2 cameraInput;

  private void Awake()
  {
    playerAttacker = GetComponent<PlayerAttacker>();
    playerInventory = GetComponent<PlayerInventory>();
    playerManager = GetComponent<PlayerManager>();
    uiManager = FindObjectOfType<UIManager>();
    cameraHandler = FindObjectOfType<CameraHandler>();
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
    ListeningToInput();
    MoveInput(delta);
    HandleRollInput(delta);
    HandleAttackInput(delta);
    HandleQuickSlotInput();
    HandleInteractingButtonInput();
    handleStartButtonInput();
    HandleLockOnButtonInput();
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
    sprintFlag = bInput;
    if (bInput)
    {
      //rollFlag = true;
      rollInputTimer += delta;
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
  private void HandleAttackInput(float delta)
  {
    if (rbInput)
    {
      if (playerManager.canDoCombo)
      {
        comboFlag = true;
        playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon);
        comboFlag = false;
      }
      else
      {
        if (playerManager.isInteracting)
          return;
        if (playerManager.canDoCombo)
          return;
        playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
      }
    }

    if (rtInput)
    {
      if (playerManager.isInteracting)
        return;
      playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
    }
  }
  private void HandleQuickSlotInput()
  {
    if (dPadRight)
    {
      Debug.Log("D pad right button is pressed");
      playerInventory.ChangeWeaponInRightHand();
    }
    else if (dPadLeft)
    {
      Debug.Log("D pad left button is pressed");
      playerInventory.ChangeWeaponInLeftHand();
    }
    else if (dPadUp)
    {
      Debug.Log("D pad up button is pressed");
    }
    else if (dPadDown)
    {
      Debug.Log("D pad down button is pressed");
    }
    
  }
  private void HandleInteractingButtonInput()
  {
    if (raInput)
    {
      Debug.Log("A button is pressed");
    }
    
  }
  private void handleStartButtonInput()
  {
    if (startInput)
    {
      Debug.Log("Start button is pressed");
      uiManager.inventoryFlag = !(uiManager.inventoryFlag);
      if (uiManager.inventoryFlag)
      {
        uiManager.OpenSelectWindow();
        uiManager.UpdateUI();
        uiManager.hudWindow.SetActive(false);
      }
      else
      {
        uiManager.CloseSelectWindow();
        uiManager.CloseAllInventoryWindows();
        uiManager.hudWindow.SetActive(true);
      }
    }
  }
  private void HandleLockOnButtonInput()
  {
    if (lockOnInput && !lockOnFlag)
    {
      cameraHandler.HandleLockOn();
      if (cameraHandler.nearestLockOnTarget != null)
      {
        Debug.Log("lock on");
        cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
        lockOnFlag = true;
      }
    }
    else if(lockOnInput && lockOnFlag)
    {
      lockOnFlag = false;
      cameraHandler.ClearLockOnTargets();
    }
    if (lockOnFlag && rightStickLeftInput)
    {
      Debug.Log("rightStickLeft");
      cameraHandler.HandleLockOn();
      if (cameraHandler.leftLockTarget != null)
      {
        cameraHandler.currentLockOnTarget = cameraHandler.leftLockTarget;
      }
      else
      {
        Debug.Log(cameraHandler.leftLockTarget.ToString());
      }
    }
    if (lockOnFlag && rightStickRightInput)
    {
      Debug.Log("rightStickRight");
      cameraHandler.HandleLockOn();
      if (cameraHandler.rightLockTarget != null)
      {
        cameraHandler.currentLockOnTarget = cameraHandler.rightLockTarget;
      }
      else
      {
        Debug.Log(cameraHandler.rightLockTarget.ToString());
      }
    }
  }
  
  private void ListeningToInput()
  {
    raInput = inputActions.PlayerAction.A.WasPressedThisFrame();
    dPadUp = inputActions.QuickSlotsInput.DPadUp.WasPressedThisFrame();
    dPadDown = inputActions.QuickSlotsInput.DPadDown.WasPressedThisFrame();
    dPadLeft = inputActions.QuickSlotsInput.DPadLeft.WasPressedThisFrame();
    dPadRight = inputActions.QuickSlotsInput.DPadRight.WasPressedThisFrame();
    rbInput = inputActions.PlayerAction.RB.WasPressedThisFrame();
    rtInput = inputActions.PlayerAction.RT.WasPressedThisFrame();
    bInput = inputActions.PlayerAction.Roll.IsPressed();
    startInput = inputActions.PlayerAction.Start.WasPressedThisFrame();
    rightStickLeftInput = inputActions.PlayerMovement.RightStickLeft.WasPressedThisFrame();
    rightStickRightInput = inputActions.PlayerMovement.RightStickRight.WasPressedThisFrame();
    lockOnInput = inputActions.PlayerAction.LockOn.WasPressedThisFrame();
  }
  
}

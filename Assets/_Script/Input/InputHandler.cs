using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Serialization;
using UnityEngine.XR;

public class InputHandler : MonoBehaviour
{
  public float horizontal;
  public float vertical;
  public float moveAmount;
  public float mouseX;
  public float mouseY;

  public bool rollFlag;
  public float rollInputTimer;
  
  public bool aInput;
  public bool bInputTap;
  public bool bInputHold;
  public bool xInput;
  public bool yInput;
  [FormerlySerializedAs("rbInput")] public bool rbTapInput;
  [FormerlySerializedAs("rtInput")] public bool rtTapInput;
  public bool ltTapInput;
  public bool ltHoldInput;
  public bool criticalHitInput;
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
  public bool twoHandFlag { get; set; }
  
  PlayerControls inputActions;
  CameraHandler cameraHandler;
  private PlayerAttacker playerAttacker;
  private PlayerInventory playerInventory;
  private PlayerManager playerManager;
  private UIManager uiManager;
  private WeaponSlotManager weaponSlotManager;
  private BlockingCollider blockingCollider;

  public Vector2 movementInput;
  Vector2 cameraInput;

  public Transform criticalAttackRaycastStartPoint;

  private void Awake()
  {
    playerInventory = GetComponent<PlayerInventory>();
    playerManager = GetComponent<PlayerManager>();
    uiManager = FindObjectOfType<UIManager>();
    cameraHandler = FindObjectOfType<CameraHandler>();
    weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
    playerAttacker = GetComponentInChildren<PlayerAttacker>();
    blockingCollider = GetComponentInChildren<BlockingCollider>();
  }

  public void OnEnable()
  {
    if (inputActions == null)
    {
      inputActions = new PlayerControls();
      inputActions.PlayerAction.Movement.performed += 
        inputActions => movementInput = inputActions.ReadValue<Vector2>();
      inputActions.PlayerAction.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
      inputActions.PlayerAction.RB.performed += i => {
        if (i.interaction is TapInteraction)
        {
          rbTapInput = true;
        }
        else if (i.interaction is HoldInteraction)
        {
          criticalHitInput = true;
        }
      }; 
      inputActions.PlayerAction.Roll.performed += i => {
        if (i.interaction is TapInteraction)
        {
          bInputTap = true;
        }
        else if (i.interaction is HoldInteraction)
        {
          bInputHold = true;
        }
      };
      inputActions.PlayerAction.Roll.canceled += i => {
        if (i.interaction is HoldInteraction)
        {
          bInputHold = false;
        }
      };
      inputActions.PlayerAction.LT.performed += i => {
        if (i.interaction is TapInteraction)
        {
          ltTapInput = true;
        }

        if (i.interaction is HoldInteraction)
        {
          ltHoldInput = true;
        }
      };
      inputActions.PlayerAction.LT.canceled += i =>  {
        if (i.interaction is HoldInteraction)
        {
          ltHoldInput = false;
        }
      };
      /*todo:
        inputActions.PlayerAction.Roll.performed += i => {
        if (i is TapInteraction)
        {
          //rollFlag = true;
        }
        else if (i is HoldInteraction)
        {
          //sprintflag = true;
        }
      }*/
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
    HandleQuickSlotInput();
    HandleInteractingButtonInput();
    HandleStartButtonInput();
    HandleLockOnButtonInput();
    HandleTwoHandInput();
    HandleCriticalHitInput();
    HandleLTInput();
  }
  public void FixedTickInput(float delta)//rb related tick inputs goes here
  {
    HandleMoveInput(delta);
    HandleRollInput();
    HandleAttackInput(delta);
  }

  private void LateUpdate()
  {
    rollFlag = false;
  }

  private void HandleMoveInput(float delta)
  {
    horizontal = movementInput.x;
    vertical = movementInput.y;
    moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
    mouseX = cameraInput.x;
    mouseY = cameraInput.y;
  }
  private void HandleRollInput()
  {
    if (bInputTap)
    {
      bInputTap = false;
      rollFlag = true;
    }
    sprintFlag = bInputHold;
  }
  private void HandleAttackInput(float delta)
  {
    if (rbTapInput)
    {
      rbTapInput = false;
      playerAttacker.HandleRbAction();
    }

    if (rtTapInput)
    {
      rtTapInput = false;
      playerAttacker.HandleRtAction();
    }
  }
  private void HandleQuickSlotInput()
  {
    if (playerManager.isInteracting) return;
    if (dPadRight)
    {
      playerInventory.ChangeWeaponInRightHand();
    }
    else if (dPadLeft)
    {
      if (twoHandFlag) return; //no left hand weapon swap when in two hand mode
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
    if (aInput)
    {
      Debug.Log("A button is pressed");
    }
    
  }
  private void HandleStartButtonInput()
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
    if (lockOnInput && !lockOnFlag)//lock on if there's no current lock on target
    {
      cameraHandler.HandleLockOn();
      if (cameraHandler.nearestLockOnTarget != null)
      {
        cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
        cameraHandler.currentLockOnTarget.GetComponentInChildren<UI_EnenmyHealthBar>().EnableHealthBar();
        lockOnFlag = true;
      }
    }
    else if(lockOnInput && lockOnFlag) //deactivate lock on if lock on is on
    {
      lockOnFlag = false;
      cameraHandler.ClearLockOnTargets();
    }
    if (lockOnFlag && rightStickLeftInput) //change lock on target
    {
      cameraHandler.HandleLockOn();
      if (cameraHandler.leftLockTarget != null)
      {
        cameraHandler.currentLockOnTarget.GetComponentInChildren<UI_EnenmyHealthBar>().DisableHealthBar();
        cameraHandler.currentLockOnTarget = cameraHandler.leftLockTarget;
        cameraHandler.currentLockOnTarget.GetComponentInChildren<UI_EnenmyHealthBar>().EnableHealthBar();
      }
      else
      {
        Debug.Log(cameraHandler.leftLockTarget.ToString());
      }
    }
    if (lockOnFlag && rightStickRightInput) //change lock on target
    {
      cameraHandler.HandleLockOn();
      if (cameraHandler.rightLockTarget != null)
      {
        cameraHandler.currentLockOnTarget.GetComponentInChildren<UI_EnenmyHealthBar>().DisableHealthBar();
        cameraHandler.currentLockOnTarget = cameraHandler.rightLockTarget;
        cameraHandler.currentLockOnTarget.GetComponentInChildren<UI_EnenmyHealthBar>().EnableHealthBar();

      }
      else
      {
        Debug.Log(cameraHandler.rightLockTarget.ToString());
      }
    }
  }
  private void HandleTwoHandInput()
  {
    if (yInput)
    {
      twoHandFlag = !twoHandFlag;
      if (twoHandFlag)
      {
        //enable two hand
        weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon,false);
      }
      else
      {
        //disable two hand
        weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon,false);
        weaponSlotManager.LoadWeaponOnSlot(playerInventory.leftWeapon,true);
      }
    }
  }
  private void HandleCriticalHitInput()
  {
    if (criticalHitInput)
    {
      criticalHitInput = false;
      playerAttacker.AttemptBackStabOrRiposte();
    }
  }
  private void HandleLTInput()
  {
    if (ltTapInput)
    {
      ltTapInput = false;
      
      if (twoHandFlag)//two hand weapon art 
      {
        Debug.Log("TH weapon art.");
      }
      else
      {
        playerAttacker.HandleLtTapAction();//parry with shield
      }
      //left hand weapon art
    }

    if (ltHoldInput)
    {
      playerAttacker.HandleLtHoldAction();
    }
    else if (!ltHoldInput)
    {
      playerManager.isBlocking = false;
      if (blockingCollider.blockingCollider.enabled)
      {
        blockingCollider.DisableBlockingCollider();
      }
    }
  }
  private void ListeningToInput()
  {
    aInput = inputActions.PlayerAction.A.WasPressedThisFrame();
    dPadUp = inputActions.QuickSlotsInput.DPadUp.WasPressedThisFrame();
    dPadDown = inputActions.QuickSlotsInput.DPadDown.WasPressedThisFrame();
    dPadLeft = inputActions.QuickSlotsInput.DPadLeft.WasPressedThisFrame();
    dPadRight = inputActions.QuickSlotsInput.DPadRight.WasPressedThisFrame();
    rtTapInput = inputActions.PlayerAction.RT.WasPressedThisFrame();
    yInput = inputActions.PlayerAction.Y.WasPressedThisFrame();
    //bInput = inputActions.PlayerAction.Roll.IsPressed();
    startInput = inputActions.PlayerAction.Start.WasPressedThisFrame();
    rightStickLeftInput = inputActions.PlayerAction.RightStickLeft.WasPressedThisFrame();
    rightStickRightInput = inputActions.PlayerAction.RightStickRight.WasPressedThisFrame();
    lockOnInput = inputActions.PlayerAction.LockOn.WasPressedThisFrame();
    //inputActions.PlayerAction.CriticalHit.performed += i => criticalHitInput = true;
    //rbInput = inputActions.PlayerAction.RB.WasPressedThisFrame();
  }
  
}

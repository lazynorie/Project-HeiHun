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
  public bool bInput;
  public bool xInput;
  public bool yInput;
  [FormerlySerializedAs("rbInput")] public bool rbTapInput;
  [FormerlySerializedAs("raInput")] public bool raTapInput;
  [FormerlySerializedAs("rtInput")] public bool rtTapInput;
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
  }

  public void OnEnable()
  {
    if (inputActions == null)
    {
      inputActions = new PlayerControls();
      inputActions.PlayerMovement.Movement.performed += 
        inputActions => movementInput = inputActions.ReadValue<Vector2>();
      inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
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
  }
  public void FixedTickInput(float delta)//rb related tick inputs goes here
  {
    HandleMoveInput(delta);
    HandleRollInput(delta);
    HandleAttackInput(delta);
  }
  private void HandleMoveInput(float delta)
  {
    horizontal = movementInput.x;
    vertical = movementInput.y;
    moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
    mouseX = cameraInput.x;
    mouseY = cameraInput.y;
  }
  private void HandleRollInput(float delta)
  {
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
    if (rbTapInput)
    {
      rbTapInput = false;
      playerAttacker.HandleRbAction();
    }

    if (rtTapInput)
    {
      if (playerManager.isInteracting)
        return;
      playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
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
    if (raTapInput)
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
  private void ListeningToInput()
  {
    raTapInput = inputActions.PlayerAction.A.WasPressedThisFrame();
    dPadUp = inputActions.QuickSlotsInput.DPadUp.WasPressedThisFrame();
    dPadDown = inputActions.QuickSlotsInput.DPadDown.WasPressedThisFrame();
    dPadLeft = inputActions.QuickSlotsInput.DPadLeft.WasPressedThisFrame();
    dPadRight = inputActions.QuickSlotsInput.DPadRight.WasPressedThisFrame();
    rtTapInput = inputActions.PlayerAction.RT.WasPressedThisFrame();
    yInput = inputActions.PlayerAction.Y.WasPressedThisFrame();
    bInput = inputActions.PlayerAction.Roll.IsPressed();
    startInput = inputActions.PlayerAction.Start.WasPressedThisFrame();
    rightStickLeftInput = inputActions.PlayerMovement.RightStickLeft.WasPressedThisFrame();
    rightStickRightInput = inputActions.PlayerMovement.RightStickRight.WasPressedThisFrame();
    lockOnInput = inputActions.PlayerAction.LockOn.WasPressedThisFrame();
    //inputActions.PlayerAction.CriticalHit.performed += i => criticalHitInput = true;
    //rbInput = inputActions.PlayerAction.RB.WasPressedThisFrame();
  }
  
}

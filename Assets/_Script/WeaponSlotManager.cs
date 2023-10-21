using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
   private PlayerManager playerManager;
   private WeaponHolderSlot leftHandSlot;
   private WeaponHolderSlot rightHandSlot;
   private WeaponHolderSlot backSlot;

   private DamageCollider leftHandDamageCollider;
   private DamageCollider rightHandDamageCollider;

   private Animator animator;

   private QuickSlotsUI quickSlotsUI;
   private InputHandler inputHandler;
   private void Awake()
   {
      playerManager = GetComponentInParent<PlayerManager>();
      quickSlotsUI = FindObjectOfType<QuickSlotsUI>();
      animator = GetComponent<Animator>();
      inputHandler = GetComponentInParent<InputHandler>();
      WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
      foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
      {
         if (weaponSlot.isLeftHandSlot)
         {
            leftHandSlot = weaponSlot;
         }
         else if (weaponSlot.isRightHandSlot)
         {
            rightHandSlot = weaponSlot;
         }
         else if (weaponSlot.isBackSlot)
         {
            backSlot = weaponSlot;
         }
      }
   }
   /// <summary>
   /// this is a function that load weapon to its slots
   /// include activate damage collider
   ///         hold idle animation
   ///         updating UI
   /// </summary>
   /// <param name="weaponItem"></param>
   /// <param name="isLeft"></param>
   public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
   {
      if (isLeft)
      {
         leftHandSlot.currentWeapon = weaponItem;
         leftHandSlot.LoadWeaponModel(weaponItem);
         LoadLeftWeaponDamageCollider();
         //update UI on the screen when swapping weapons
         quickSlotsUI.UpdateWeaponQuickSlotsUI(true,weaponItem);
         #region handle left hand idel animation
         if (weaponItem != null)
         {
            animator.CrossFade(weaponItem.left_hand_idle,0.2f);
         }
         else
         {
            animator.CrossFade("Left Arm Empty State", 0.2f);
         }
         #endregion
      }
      else
      {
         if (inputHandler.twoHandFlag)
         {
            //Move current left hand weapon to back 
            backSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
            leftHandSlot.UnloadWeaponAndDestroy();
            animator.CrossFade(weaponItem.th_idle, 0.2f);
         }
         else
         {
            #region Handle right hand idle animation
            animator.CrossFade("Both Arms Empty", 0.2f);
            backSlot.UnloadWeaponAndDestroy();
            if (weaponItem != null)
            {
               animator.CrossFade(weaponItem.right_hand_idle,0.2f);
            }
            else
            {
               animator.CrossFade("Right Arm Empty State", 0.2f);
            }
            #endregion
         }

         rightHandSlot.currentWeapon = weaponItem;
         rightHandSlot.LoadWeaponModel(weaponItem);
         LoadRightWeaponDamageCollider();
         //update UI on the screen when swapping weapons
         quickSlotsUI.UpdateWeaponQuickSlotsUI(false,weaponItem);
      }
   }
   #region Handle Weapon Damage Collider
   private void LoadLeftWeaponDamageCollider()
   {
      leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
   }
   private void LoadRightWeaponDamageCollider()
   {
      rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
   }
   #region handling damagecollider 
   public void OpenDamageCollider()
   {
      if (playerManager.isUsingRightHand)
      {
         rightHandDamageCollider.EnableDamageCollider();
      }
      if (playerManager.isUsingLeftHand)
      {
         leftHandDamageCollider.EnableDamageCollider();
      }
   }
   public void CloseDamageCollider()
   {
      if (playerManager.isUsingRightHand)
      {
         rightHandDamageCollider.DisableDamageCollider();
      }
      if (playerManager.isUsingLeftHand)
      {
         leftHandDamageCollider.DisableDamageCollider();
      }
   }
   #endregion
   #endregion
}

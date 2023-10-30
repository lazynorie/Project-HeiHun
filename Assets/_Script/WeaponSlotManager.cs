using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
   private PlayerManager playerManager;
   private WeaponHolderSlot leftHandSlot;
   public WeaponHolderSlot rightHandSlot;
   private WeaponHolderSlot backSlot;
   private WeaponHolderSlot shieldSlot;
   private PlayerInventory playerInventory;
   public WeaponHolderSlot[] weaponHolderSlots;
   
   private DamageCollider leftHandDamageCollider;
   public DamageCollider rightHandDamageCollider;

   private Animator animator;

   private QuickSlotsUI quickSlotsUI;
   private InputHandler inputHandler;
   private void Awake()
   {
      playerInventory = GetComponentInParent<PlayerInventory>();
      playerManager = GetComponentInParent<PlayerManager>();
      quickSlotsUI = FindObjectOfType<QuickSlotsUI>();
      animator = GetComponent<Animator>();
      inputHandler = GetComponentInParent<InputHandler>();
      weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
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
         else if (weaponSlot.isShieldSlot)
         {
            shieldSlot = weaponSlot;
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
            //Move current left hand weapon/shield to back 
            if (playerInventory.leftWeapon.weaponType is WeaponType.Shield)
            {
               shieldSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
            }
            else if (playerInventory.leftWeapon.weaponType is WeaponType.MeleeWeapon)
            {
               backSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
            }


            leftHandSlot.UnloadWeaponAndDestroy();
            animator.CrossFade(weaponItem.th_idle, 0.2f);
         }
         else
         {
            #region Handle right hand idle animation
            animator.CrossFade("Both Arms Empty", 0.2f);
            backSlot.UnloadWeaponAndDestroy();
            shieldSlot.UnloadWeaponAndDestroy();
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
   public void LoadBothWeaponOnSlot()
   {
      LoadWeaponOnSlot(playerInventory.rightWeapon,false);
      LoadWeaponOnSlot(playerInventory.leftWeapon, true);
   }

   public void ReloadRightHandWeaponOnSlot()
   {
      LoadWeaponOnSlot(playerInventory.rightWeapon,false);
   }
   #region Handle Weapon Damage Collider
   private void LoadLeftWeaponDamageCollider()
   {
      leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
      if (leftHandDamageCollider != null)
      {
         leftHandDamageCollider.weaponOwner = GetComponentInParent<CharacterManager>();
         leftHandDamageCollider.weapondamage =
            playerInventory.leftWeapon.baseDamage; //assign damage to damage collider}
      }
      else Debug.Log("there's no dmg collider assigned to this weapon");
   }
   private void LoadRightWeaponDamageCollider()
   {
      rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
      rightHandDamageCollider.weaponOwner = GetComponentInParent<CharacterManager>();

      if (rightHandDamageCollider != null)
      {
         rightHandDamageCollider.weapondamage = playerInventory.rightWeapon.baseDamage;
      }
      else Debug.Log("there's no dmg collider assigned to this weapon");
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
   private WeaponHolderSlot leftHandSlot;
   private WeaponHolderSlot rightHandSlot;

   private DamageCollider lefthanddamagecollider;
   private DamageCollider righthanddamagecollider;

   private Animator animator;

   private QuickSlotsUI quickSlotsUI;
   private void Awake()
   {
      quickSlotsUI = FindObjectOfType<QuickSlotsUI>();
      animator = GetComponent<Animator>();
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
      }
   }

   #region Handle Weapon Damage Collider
   public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
   {
      if (isLeft)
      {
         leftHandSlot.LoadWeaponModel(weaponItem);
         LoadLeftWeaponDamageCollider();
         //update UI on the screen when swapping weapons
         quickSlotsUI.UpdateWeaponQuickSlotsUI(true,weaponItem);
         #region handle left hand idel animation
         /*if (weaponItem != null)
                  {
                     animator.CrossFade(weaponItem.left_hand_idle,0.2f);
                  }
                  else
                  {
                     animator.CrossFade("Left Arm Empty State", 0.2f);
                  }*/
         #endregion
      }
      else
      {
         rightHandSlot.LoadWeaponModel(weaponItem);
         LoadRightWeaponDamageCollider();
         //update UI on the screen when swapping weapons
         quickSlotsUI.UpdateWeaponQuickSlotsUI(false,weaponItem);
         #region Handle right hand idle animation
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
   }
   private void LoadLeftWeaponDamageCollider()
   {
      lefthanddamagecollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
   }
   private void LoadRightWeaponDamageCollider()
   {
      righthanddamagecollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
   }

   #region handling damagecollider 
public void OpenRightDamageCollider()
   {
      righthanddamagecollider.EnableDamageCollider();
   }
   public void OpenLeftDamageCollider()
   {
      lefthanddamagecollider.EnableDamageCollider();
   }
   public void CloseRightDamageCollider()
   {
      righthanddamagecollider.DisableDamageCollider();
   }
   public void CloseLeftDamageCollider()
   {
      lefthanddamagecollider.DisableDamageCollider();
   }
   

   #endregion
   
   #endregion
}

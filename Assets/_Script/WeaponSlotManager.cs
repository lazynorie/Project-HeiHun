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
   private void Awake()
   {
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
      }
      else
      {
         rightHandSlot.LoadWeaponModel(weaponItem);
         LoadRightWeaponDamageCollider();
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
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
  private WeaponSlotManager weaponSlotManager;
  public WeaponItem rightWeapon;
  public WeaponItem leftWeapon;
  public WeaponItem unarmedWeapon;
  
  [Header("Equipped Weapons Inventory")]
  private int currentRightWeaponIndex = -1;
  public WeaponItem[] rightHandWeaponSlots = new WeaponItem[3];
  private int currentLeftWeaponIndex = -1;
  public WeaponItem[] leftHandWeaponSlots = new WeaponItem[3];
  [Header("Inactive Weapon Inventory")]
//list for player weapons
  public List<WeaponItem> weaponInventory;
  
  private void Awake()
  {
    weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
  }

  private void Start()
  {
    LoadFirstWeaponsInInventory();
  }

  public void ChangeWeaponInRightHand()
  {
    currentRightWeaponIndex = currentRightWeaponIndex + 1;
    if (currentRightWeaponIndex == 0 && rightHandWeaponSlots[0]!=null)
    {
      rightWeapon = rightHandWeaponSlots[currentRightWeaponIndex];
      weaponSlotManager.LoadWeaponOnSlot(rightHandWeaponSlots[currentRightWeaponIndex], false);
    }
    else if (currentRightWeaponIndex ==0 && rightHandWeaponSlots[0] == null)
    {
      currentRightWeaponIndex += 1;
    }
    else if (currentRightWeaponIndex == 1&& rightHandWeaponSlots[1]!=null)
    {
      rightWeapon = rightHandWeaponSlots[currentRightWeaponIndex];
      weaponSlotManager.LoadWeaponOnSlot(rightHandWeaponSlots[currentRightWeaponIndex],false);
    }
    else if(currentRightWeaponIndex == 1 && rightHandWeaponSlots[1]==null)
    {
      currentRightWeaponIndex += 1;
    }
    
    else if (currentRightWeaponIndex == 2&& rightHandWeaponSlots[2]!=null)
    {
      rightWeapon = rightHandWeaponSlots[currentRightWeaponIndex];
      weaponSlotManager.LoadWeaponOnSlot(rightHandWeaponSlots[currentRightWeaponIndex],false);
    }
    else if(currentRightWeaponIndex == 2 && rightHandWeaponSlots[2]==null)
    {
      currentRightWeaponIndex += 1;
    }
    
    else if (currentRightWeaponIndex == 3&& rightHandWeaponSlots[3]!=null)
    {
      rightWeapon = rightHandWeaponSlots[currentRightWeaponIndex];
      weaponSlotManager.LoadWeaponOnSlot(rightHandWeaponSlots[currentRightWeaponIndex],false);
    }
    else if(currentRightWeaponIndex == 3 && rightHandWeaponSlots[3]==null)
    {
      currentRightWeaponIndex += 1;
    }

    if (currentRightWeaponIndex > rightHandWeaponSlots.Length -1)
    {
      currentRightWeaponIndex = -1;
      rightWeapon = unarmedWeapon;
      weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon,false);
    }
  }
  
  public void ChangeWeaponInLeftHand()
  {
    currentLeftWeaponIndex = currentLeftWeaponIndex + 1;
    if (currentLeftWeaponIndex == 0 && leftHandWeaponSlots[0]!=null)
    {
      leftWeapon = leftHandWeaponSlots[currentLeftWeaponIndex];
      weaponSlotManager.LoadWeaponOnSlot(leftHandWeaponSlots[currentLeftWeaponIndex], true);
    }
    else if (currentLeftWeaponIndex ==0 && leftHandWeaponSlots[0] == null)
    {
      currentLeftWeaponIndex += 1;
    }
    else if (currentLeftWeaponIndex == 1&& leftHandWeaponSlots[1]!=null)
    {
      leftWeapon = leftHandWeaponSlots[currentLeftWeaponIndex];
      weaponSlotManager.LoadWeaponOnSlot(leftHandWeaponSlots[currentLeftWeaponIndex],true);
    }
    else if(currentLeftWeaponIndex == 1 && leftHandWeaponSlots[1]==null)
    {
      currentLeftWeaponIndex += 1;
    }
    
    else if (currentLeftWeaponIndex == 2&& leftHandWeaponSlots[2]!=null)
    {
      leftWeapon = leftHandWeaponSlots[currentLeftWeaponIndex];
      weaponSlotManager.LoadWeaponOnSlot(leftHandWeaponSlots[currentLeftWeaponIndex],true);
    }
    else if(currentLeftWeaponIndex == 2 && leftHandWeaponSlots[2]==null)
    {
      currentLeftWeaponIndex += 1;
    }
    
    else if (currentLeftWeaponIndex == 3&& leftHandWeaponSlots[3]!=null)
    {
      leftWeapon = leftHandWeaponSlots[currentLeftWeaponIndex];
      weaponSlotManager.LoadWeaponOnSlot(leftHandWeaponSlots[currentLeftWeaponIndex],true);
    }
    else if(currentLeftWeaponIndex == 3 && leftHandWeaponSlots[3]==null)
    {
      currentLeftWeaponIndex += 1;
    }

    if (currentLeftWeaponIndex > leftHandWeaponSlots.Length -1)
    {
      currentLeftWeaponIndex = -1;
      leftWeapon = unarmedWeapon;
      weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon,true);
    }
  }
  
  private void LoadFirstWeaponsInInventory()
  {
    rightWeapon = rightHandWeaponSlots[0];
    weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
    leftWeapon = leftHandWeaponSlots[0];
    weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
  }
}

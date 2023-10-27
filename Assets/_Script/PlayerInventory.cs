using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
  private WeaponSlotManager weaponSlotManager;
  public SpellItem currentSpell;
  public WeaponItem rightWeapon;
  public WeaponItem leftWeapon;
  public WeaponItem unarmedWeapon;
  
  [Header("Equipped Weapons Inventory")]
  public int currentRightWeaponIndex = -1;
  public WeaponItem[] weaponsInRightHandSlots = new WeaponItem[4];
  public int currentLeftWeaponIndex = -1;
  public WeaponItem[] weaponsInLeftHandSlots = new WeaponItem[4];
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
    if (currentRightWeaponIndex == 0 && weaponsInRightHandSlots[0]!=null)
    {
      rightWeapon = weaponsInRightHandSlots[currentRightWeaponIndex];
      weaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[currentRightWeaponIndex], false);
    }
    else if (currentRightWeaponIndex ==0 && weaponsInRightHandSlots[0] == null)
    {
      currentRightWeaponIndex += 1;
    }
    else if (currentRightWeaponIndex == 1&& weaponsInRightHandSlots[1]!=null)
    {
      rightWeapon = weaponsInRightHandSlots[currentRightWeaponIndex];
      weaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[currentRightWeaponIndex],false);
    }
    else if(currentRightWeaponIndex == 1 && weaponsInRightHandSlots[1]==null)
    {
      currentRightWeaponIndex += 1;
    }
    else if (currentRightWeaponIndex == 2&& weaponsInRightHandSlots[2]!=null)
    {
      rightWeapon = weaponsInRightHandSlots[currentRightWeaponIndex];
      weaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[currentRightWeaponIndex],false);
    }
    else if(currentRightWeaponIndex == 2 && weaponsInRightHandSlots[2]==null)
    {
      currentRightWeaponIndex += 1;
    }
    else if (currentRightWeaponIndex == 3&& weaponsInRightHandSlots[3]!=null)
    {
      rightWeapon = weaponsInRightHandSlots[currentRightWeaponIndex];
      weaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[currentRightWeaponIndex],false);
    }
    else if(currentRightWeaponIndex == 3 && weaponsInRightHandSlots[3]==null)
    {
      currentRightWeaponIndex += 1;
    }
    if (currentRightWeaponIndex > weaponsInRightHandSlots.Length -1)
    {
      currentRightWeaponIndex = -1;
      rightWeapon = unarmedWeapon;
      weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon,false);
    }
  }
  
  public void ChangeWeaponInLeftHand()
  {
    currentLeftWeaponIndex = currentLeftWeaponIndex + 1;
    if (currentLeftWeaponIndex == 0 && weaponsInLeftHandSlots[0]!=null)
    {
      leftWeapon = weaponsInLeftHandSlots[currentLeftWeaponIndex];
      weaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlots[currentLeftWeaponIndex], true);
    }
    else if (currentLeftWeaponIndex ==0 && weaponsInLeftHandSlots[0] == null)
    {
      currentLeftWeaponIndex += 1;
    }
    else if (currentLeftWeaponIndex == 1&& weaponsInLeftHandSlots[1]!=null)
    {
      leftWeapon = weaponsInLeftHandSlots[currentLeftWeaponIndex];
      weaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlots[currentLeftWeaponIndex],true);
    }
    else if(currentLeftWeaponIndex == 1 && weaponsInLeftHandSlots[1]==null)
    {
      currentLeftWeaponIndex += 1;
    }
    
    else if (currentLeftWeaponIndex == 2&& weaponsInLeftHandSlots[2]!=null)
    {
      leftWeapon = weaponsInLeftHandSlots[currentLeftWeaponIndex];
      weaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlots[currentLeftWeaponIndex],true);
    }
    else if(currentLeftWeaponIndex == 2 && weaponsInLeftHandSlots[2]==null)
    {
      currentLeftWeaponIndex += 1;
    }
    
    else if (currentLeftWeaponIndex == 3&& weaponsInLeftHandSlots[3]!=null)
    {
      leftWeapon = weaponsInLeftHandSlots[currentLeftWeaponIndex];
      weaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlots[currentLeftWeaponIndex],true);
    }
    else if(currentLeftWeaponIndex == 3 && weaponsInLeftHandSlots[3]==null)
    {
      currentLeftWeaponIndex += 1;
    }

    if (currentLeftWeaponIndex > weaponsInLeftHandSlots.Length -1)
    {
      currentLeftWeaponIndex = -1;
      leftWeapon = unarmedWeapon;
      weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon,true);
    }
  }
  
  private void LoadFirstWeaponsInInventory()
  {
    currentRightWeaponIndex = 1;
    rightWeapon = weaponsInRightHandSlots[0];
    weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
    currentLeftWeaponIndex = 1;
    leftWeapon = weaponsInLeftHandSlots[0];
    weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
  }
}

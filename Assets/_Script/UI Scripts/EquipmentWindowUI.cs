using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentWindowUI : MonoBehaviour
{
    public bool rightHandSlot01;
    public bool rightHandSlot02;
    public bool rightHandSlot03;
    public bool rightHandSlot04;
    public bool leftHandSlot01;
    public bool leftHandSlot02;
    public bool leftHandSlot03;
    public bool leftHandSlot04;
    private EquipmentSlot[] equipmentInHandSlot;

    private void Start()
    {
        equipmentInHandSlot = GetComponentsInChildren<EquipmentSlot>();
    }

    public void LoadWeaponsOnQuipmentScreen(PlayerInventory playerInventory)
    {
        for (int i = 0; i < equipmentInHandSlot.Length; i++)
        {
            if (equipmentInHandSlot[i] != null) {
                //right hand equipment slots
                if (equipmentInHandSlot[i].rightHandSlot01)
                {
                    equipmentInHandSlot[i].AddItem(playerInventory.weaponsInRightHandSlots[0]);
                }
                else if (equipmentInHandSlot[i].rightHandSlot02)
                {
                    equipmentInHandSlot[i].AddItem(playerInventory.weaponsInRightHandSlots[1]);
                }
                else if (equipmentInHandSlot[i].rightHandSlot03)
                {
                    equipmentInHandSlot[i].AddItem(playerInventory.weaponsInRightHandSlots[2]);
                }
                else if (equipmentInHandSlot[i].rightHandSlot04)
                {
                    equipmentInHandSlot[i].AddItem(playerInventory.weaponsInRightHandSlots[3]);
                }
            
                //left hand equipment slot
                else if (equipmentInHandSlot[i].leftHandSlot01)
                {
                    equipmentInHandSlot[i].AddItem(playerInventory.weaponsInLeftHandSlots[0]);
                }
                else if (equipmentInHandSlot[i].leftHandSlot02)
                {
                    equipmentInHandSlot[i].AddItem(playerInventory.weaponsInLeftHandSlots[1]);
                }
                else if (equipmentInHandSlot[i].leftHandSlot03)
                {
                    equipmentInHandSlot[i].AddItem(playerInventory.weaponsInLeftHandSlots[2]);
                }
                else if (equipmentInHandSlot[i].leftHandSlot04)
                {
                    equipmentInHandSlot[i].AddItem(playerInventory.weaponsInLeftHandSlots[3]);
                }
            }
            else return;
        }
    }

    #region Connect in Editor
    public void SelectRightHandSlot01()
    {
        rightHandSlot01 = true;
    }
    public void SelectRightHandSlot02()
    {
        rightHandSlot02 = true;
    }
    public void SelectRightHandSlot03()
    {
        rightHandSlot03 = true;
    }
    public void SelectRightHandSlot04()
    {
        rightHandSlot04 = true;
    }
    public void SelectLeftHandSlot01()
    {
        leftHandSlot01 = true;
    }
    public void SelectLeftHandSlot02()
    {
        leftHandSlot02 = true;
    }
    public void SelectLeftHandSlot03()
    {
        leftHandSlot03 = true;
    }
    public void SelectLeftHandSlot04()
    {
        leftHandSlot04 = true;
    }
    #endregion
}

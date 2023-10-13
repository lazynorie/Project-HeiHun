using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private PlayerInventory playerInventory;
    public EquipmentWindowUI equipmentWindowUI;
    

    [Header("UI windows")]
    public GameObject hudWindow; 
    public GameObject weaponInventoryWindow;
    public GameObject selectWindow;
    public GameObject equipmentWindow;
    [HideInInspector]
    public bool inventoryFlag;

    [Header("Equipment Window Slot Selected")]
    public bool[] rightHandWeaponSlotsSelected;
    public bool[] leftHandWeaponSlotsSelected;
    

    [Header("Weapon Inventory")]
    public GameObject weaponInventorySlotPrefab;
    public Transform weaponIventorySlotsParent;
    private WeaponInventorySlot[] weaponInventorySlots;

    private void Awake()
    {
        equipmentWindowUI = FindObjectOfType<EquipmentWindowUI>();
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    private void Start()
    {
        //inventoryFlag = false;
        rightHandWeaponSlotsSelected = new bool[4];
        leftHandWeaponSlotsSelected = new bool[4];
        equipmentWindowUI.LoadWeaponsOnQuipmentScreen(playerInventory);
        weaponInventorySlots = weaponIventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
        DisplayPlayerHUD();
        ClearAllInventoryWindows();
        EquipmentSlot.onButtonClicked += SetCurrentWeaponSlot;
    }
    public void UpdateUI()
    {
        #region Weapon Inventory
        for (int i = 0; i < weaponInventorySlots.Length; i++)
        {
            if (i<playerInventory.weaponInventory.Count)
            {
                if (weaponInventorySlots.Length < playerInventory.weaponInventory.Count)
                {
                    Instantiate(weaponInventorySlotPrefab, weaponIventorySlotsParent);
                    weaponInventorySlots = weaponIventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
                }
                weaponInventorySlots[i].AddItem(playerInventory.weaponInventory[i]);
            }
            else
            {
                weaponInventorySlots[i].ClearInventorySlot();
            }
        }
        #endregion
    }
    public void OpenSelectWindow()
    {
        selectWindow.SetActive(true);
    }
    public void CloseSelectWindow()
    {
        selectWindow.SetActive(false);
    }

    public void CloseAllInventoryWindows()
    {
        weaponInventoryWindow.SetActive(false);
        equipmentWindow.SetActive(false);
        ResetAllSelectedSlot();//call this after the equipments slots arrays are created
    }

    private void ClearAllInventoryWindows()
    {
        CloseAllInventoryWindows();
        CloseSelectWindow();
    }

    private void DisplayPlayerHUD()
    {
        hudWindow.SetActive(true);
    }

    private void SetCurrentWeaponSlot(int id)
    {
        if (id == 11)
        {
            rightHandWeaponSlotsSelected[0] = true;
        }
        if (id == 12)
        {
            rightHandWeaponSlotsSelected[1] = true;

        }
        if (id == 13)
        {
            rightHandWeaponSlotsSelected[2] = true;

        }
        if (id == 14)
        {
            rightHandWeaponSlotsSelected[3] = true;
        }
        if (id == 21)
        {
            leftHandWeaponSlotsSelected[0] = true;
        }
        if (id == 22)
        {
            leftHandWeaponSlotsSelected[1] = true;
        }
        if (id == 23)
        {
            leftHandWeaponSlotsSelected[2] = true;
        }
        if (id == 24)
        {
            leftHandWeaponSlotsSelected[3] = true;
        }
    }

    public void ResetAllSelectedSlot()
    {
        for (int i = 0; i < rightHandWeaponSlotsSelected.Length; i++)
        {
            rightHandWeaponSlotsSelected[i] = false;
        }

        for (int i = 0; i < leftHandWeaponSlotsSelected.Length; i++)
        {
            leftHandWeaponSlotsSelected[i] = false;
        }
    }
}

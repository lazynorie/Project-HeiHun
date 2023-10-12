using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private PlayerInventory playerInventory;
    private EquipmentWindowUI equipmentWindowUI;

    [Header("UI windows")]
    public GameObject hudWindow; 
    public GameObject weaponInventoryWindow;
    public GameObject selectWindow;
    public GameObject equipmentWindow;
    [HideInInspector]
    public bool inventoryFlag;

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
        equipmentWindowUI.LoadWeaponsOnQuipmentScreen(playerInventory);
        weaponInventorySlots = weaponIventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
        DisplayPlayerHUD();
        ClearAllInventoryWindows();
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
}

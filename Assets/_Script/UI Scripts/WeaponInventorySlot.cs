using UnityEngine;
using UnityEngine.UI;

public class WeaponInventorySlot : MonoBehaviour
{
    private PlayerInventory playerInventory;
    private WeaponSlotManager weaponSlotManager;
    private UIManager uiManager;
    public Image icon;
    private WeaponItem item;

    private void Awake()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        uiManager = FindObjectOfType<UIManager>();
        weaponSlotManager = FindObjectOfType<WeaponSlotManager>();
    }

    public void AddItem(WeaponItem newItem)
    {
        item = newItem;
        icon.sprite = item.itemIcon;
        icon.enabled = true;
        gameObject.SetActive(true);
    }
    public void ClearInventorySlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        gameObject.SetActive(false);
    }

    public void EquipThisItem()
    {
        for (int i = 0; i < uiManager.rightHandWeaponSlotsSelected.Length; i++)
        {
            if (uiManager.rightHandWeaponSlotsSelected[i])
            {
                if (playerInventory.weaponsInRightHandSlots[i] != null)
                {
                    playerInventory.weaponInventory.Add(playerInventory.weaponsInRightHandSlots[i]);
                    playerInventory.weaponsInRightHandSlots[i] = item;
                    playerInventory.weaponInventory.Remove(item);
                }
                else
                {
                    Debug.Log("You don't have an item in this slot");
                }
            }
        }
        for (int i = 0; i < uiManager.leftHandWeaponSlotsSelected.Length; i++)
        {
            if (uiManager.leftHandWeaponSlotsSelected[i])
            {
                if (playerInventory.weaponsInLeftHandSlots[i] != null)
                {
                    playerInventory.weaponInventory.Add(playerInventory.weaponsInLeftHandSlots[i]);
                    playerInventory.weaponsInLeftHandSlots[i] = item;
                    playerInventory.weaponInventory.Remove(item);
                    
                }
                else
                {
                    Debug.Log("You don't have an item in this slot");
                }
            }
        }
        //remove current item
        //add current item to inventory
        //equip this new item
        //remove this item from inventory
        playerInventory.rightWeapon =
            playerInventory.weaponsInRightHandSlots[playerInventory.currentRightWeaponIndex];
        playerInventory.leftWeapon =
            playerInventory.weaponsInLeftHandSlots[playerInventory.currentLeftWeaponIndex];
        weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon,false);
        weaponSlotManager.LoadWeaponOnSlot(playerInventory.leftWeapon,true);
        
        uiManager.equipmentWindowUI.LoadWeaponsOnQuipmentScreen(playerInventory);
        uiManager.ResetAllSelectedSlot();
        uiManager.UpdateUI();
    }
}

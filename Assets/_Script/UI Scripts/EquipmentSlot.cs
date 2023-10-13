using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class EquipmentSlot : MonoBehaviour
{
    public Image icon;
    private WeaponItem weaponItem;
    [SerializeField]private int sLotID;

    public bool rightHandSlot01;
    public bool rightHandSlot02;
    public bool rightHandSlot03;
    public bool rightHandSlot04;

    public bool leftHandSlot01;
    public bool leftHandSlot02;
    public bool leftHandSlot03;
    public bool leftHandSlot04;

    public static event Action<int> onButtonClicked; 

    public void AddItem(WeaponItem newWeapon)
    {
        if (newWeapon != null)
        {
            weaponItem = newWeapon;
            icon.sprite = weaponItem.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }
        else return;
    }
    public void ClearItem()
    {
        weaponItem = null;
        icon.sprite = null;
        icon.enabled = false;
        gameObject.SetActive(false);
    }

    public void OnButtonClicked()
    {
        onButtonClicked?.Invoke(sLotID);
    }
}


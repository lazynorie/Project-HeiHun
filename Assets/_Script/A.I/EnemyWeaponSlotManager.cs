using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyWeaponSlotManager : MonoBehaviour
{
    private WeaponHolderSlot rightHandSlot;
    private WeaponHolderSlot leftHandSlot;
    private WeaponItem rightHandWeapon;
    private WeaponItem leftHandWeapon;

    private DamageCollider leftHandDamageCollider;
    private DamageCollider rightHandDamageCollider;
    
    public WeaponHolderSlot[] weaponHolderSlots;
    
    private void Awake()
    {
        weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
        /*playerManager = GetComponentInParent<PlayerManager>();
        quickSlotsUI = FindObjectOfType<QuickSlotsUI>();
        animator = GetComponent<Animator>();
        inputHandler = GetComponentInParent<InputHandler>();*/
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
            /*else if (weaponSlot.isBackSlot)
            {
                backSlot = weaponSlot;
            }*/
        }
    }

    private void Start()
    {
        GetWeaponFromInventory();
        LoadWeapons();
    }

    public void LoadWeaponOnSlot(WeaponItem weapon, bool isleft)
    {
        if (isleft)
        {
            leftHandSlot.currentWeapon = weapon;
            leftHandSlot.LoadWeaponModel(weapon);
            LoadWeaponDamageCollider(true);
        }
        else
        {
            rightHandSlot.currentWeapon = weapon;
            rightHandSlot.LoadWeaponModel(weapon);
            LoadWeaponDamageCollider(false);
        }
    }

    public void LoadWeaponDamageCollider(bool isLeft)
    {
        if (isLeft)
        {
            leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            leftHandDamageCollider.characterManager = GetComponentInParent<CharacterManager>();
        }
        else
        {
            rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            rightHandDamageCollider.characterManager = GetComponentInParent<CharacterManager>();
        }
    }

    public void OpenDamageCollider()
    {
        rightHandDamageCollider.EnableDamageCollider();
    }
    public void CloseDamageCollider()
    {
        rightHandDamageCollider.DisableDamageCollider();
    }
    

    public void LoadWeapons()
    {
        if (rightHandWeapon != null)
        {
            LoadWeaponOnSlot(rightHandWeapon, false);
        }

        if (leftHandWeapon != null)
        {
            LoadWeaponOnSlot(leftHandWeapon, true);
        }
    }

    public void GetWeaponFromInventory()
    {
        rightHandWeapon = GetComponentInParent<WeaponInventory>().weaponItems[0];
        
    }
}

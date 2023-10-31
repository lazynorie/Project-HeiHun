using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerVFXManager : MonoBehaviour
{
    private PlayerStats playerStats;
    private WeaponSlotManager weaponSlotManager;
    private InputHandler inputHandler;
    [Tooltip("The vfx effect playing right now")]
    public GameObject currentVFX;

    public GameObject instantiatedVFXmodel;
    public int restoringHPAmount;
    public int restoringManaAmount;

    private void Awake()
    {
        playerStats = GetComponentInParent<PlayerStats>();
        weaponSlotManager = GetComponent<WeaponSlotManager>();
        inputHandler = GetComponentInParent<InputHandler>();
    }

    public void HealPlayerFromEffect()
    {
        playerStats.RestorePlayerHealthPoints(restoringHPAmount);
        playerStats.RestorePlayerManaPoints(restoringManaAmount);
        //todo: GameObject healingParticles = Instantiate(currentVFX, playerStats.transform);
        Destroy(instantiatedVFXmodel.gameObject);
        if (inputHandler.twoHandFlag)//load right hand weapon only if player is 2h
        {
            weaponSlotManager.ReloadRightHandWeaponOnSlot();
            return;
        }
        weaponSlotManager.LoadBothWeaponOnSlot();
    }
}

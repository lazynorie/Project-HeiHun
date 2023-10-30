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
    public int restoringAmount;

    private void Awake()
    {
        playerStats = GetComponentInParent<PlayerStats>();
        weaponSlotManager = GetComponent<WeaponSlotManager>();
        inputHandler = GetComponentInParent<InputHandler>();
    }

    public void HealPlayerFromEffect()
    {
        playerStats.HealPlayer(restoringAmount);
        //todo: GameObject healingParticles = Instantiate(currentVFX, playerStats.transform);
        Destroy(instantiatedVFXmodel.gameObject);
        if (inputHandler.twoHandFlag)
        {
            weaponSlotManager.ReloadRightHandWeaponOnSlot();
            return;
        }
        weaponSlotManager.LoadBothWeaponOnSlot();
    }
}

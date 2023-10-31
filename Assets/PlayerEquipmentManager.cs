using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    public BlockingCollider blockingCollider;
    private InputHandler inputHandler;
    private PlayerInventory playerInventory;
    [Header("Equipment Changer")] private HelmetSlotManager helmetSlotManager;
    private void Awake()
    {
        //blockingCollider = GetComponentInChildren<BlockingCollider>();
        inputHandler = GetComponentInParent<InputHandler>();
        playerInventory = GetComponentInParent<PlayerInventory>();
        helmetSlotManager = GetComponentInChildren<HelmetSlotManager>();
    }

    private void Start()
    {
        helmetSlotManager.UnequipAllHelmetModels();
        helmetSlotManager.EquipHelmetModelByName(playerInventory.currentHelmet.helmetModelName);
    }

    public void OpenBlockingCollider()
    {
        if (inputHandler.twoHandFlag)
        {
            blockingCollider.SetDamageAbsorption(playerInventory.rightWeapon);
        }
        else
        {
            blockingCollider.SetDamageAbsorption(playerInventory.leftWeapon);
        }
        //blockingCollider.SetDamageAbsorption();
        blockingCollider.EnableBlockingCollider();
    }

    public void CloseBlockingCollider()
    {
        blockingCollider.DisableBlockingCollider();
    }
}

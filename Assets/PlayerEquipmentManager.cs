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
    private RingSlotManager[] ringSlotManagers;
    private RingSlotManager leftRingSlot;
    private RingSlotManager rightRingSlot;

    private void Awake()
    {
        //blockingCollider = GetComponentInChildren<BlockingCollider>();
        inputHandler = GetComponentInParent<InputHandler>();
        playerInventory = GetComponentInParent<PlayerInventory>();
        helmetSlotManager = GetComponentInChildren<HelmetSlotManager>();
        ringSlotManagers = GetComponentsInChildren<RingSlotManager>();
        AssignRingSlots();
    }

    private void Start()
    {
        helmetSlotManager.UnequipAllHelmetModels();
        helmetSlotManager.EquipHelmetModelByName(playerInventory.currentHelmet.helmetModelName);
        leftRingSlot.UnequipAllRingsModels();
        leftRingSlot.EquipRingModelByName(playerInventory.currentLeftRing.RingModelName);
        rightRingSlot.UnequipAllRingsModels();
        rightRingSlot.EquipRingModelByName(playerInventory.currentRightRing.RingModelName);
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

    private void AssignRingSlots()
    {
        foreach (var manager in ringSlotManagers)
        {
            if (manager.isLeft)
            {
                leftRingSlot = manager;
            }
            else
            {
                rightRingSlot = manager;
            }
        }
    }

}

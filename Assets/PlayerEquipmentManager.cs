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
        EquipAllEquipmentOnSlots();
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

    /// <summary>
    /// simply assign rings slots to left hand ringslot or righthand ringslot
    /// </summary>
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

    private void EquipAllEquipmentOnSlots()
    {
        if (playerInventory.currentHelmet != null)
        {
            helmetSlotManager.UnequipAllHelmetModels();
            helmetSlotManager.EquipHelmetModelByName(playerInventory.currentHelmet.helmetModelName);
        }
        if (playerInventory.currentLeftRing != null)
        {
            leftRingSlot.UnequipAllRingsModels();
            leftRingSlot.EquipRingModelByName(playerInventory.currentLeftRing.RingModelName);
        }
        if (playerInventory.currentRightRing != null)
        {
            rightRingSlot.UnequipAllRingsModels();
            rightRingSlot.EquipRingModelByName(playerInventory.currentRightRing.RingModelName);
        }
    }
}

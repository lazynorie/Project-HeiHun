using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    public BlockingCollider blockingCollider;
    private InputHandler inputHandler;
    private PlayerInventory playerInventory;
    private void Awake()
    {
        //blockingCollider = GetComponentInChildren<BlockingCollider>();
        inputHandler = GetComponentInParent<InputHandler>();
        playerInventory = GetComponentInParent<PlayerInventory>();
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

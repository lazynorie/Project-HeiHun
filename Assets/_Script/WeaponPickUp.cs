using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : Interactable
{
   public WeaponItem weapon;

    public override void Interact(PlayerManager playerManager)
    {
        base.Interact(playerManager);
        //pick up a weapon and add it to player inventory
        PickUpItem(playerManager);
    }

    private void PickUpItem(PlayerManager playerManager)
    {
        PlayerInventory playerInventory;
        PlayerLocalmotion playerLocomotion;
        AnimationHandler animationHandler;

        playerInventory = playerManager.GetComponent<PlayerInventory>();
        playerLocomotion = playerManager.GetComponent<PlayerLocalmotion>();
        animationHandler = playerManager.GetComponentInChildren<AnimationHandler>();

        playerLocomotion.rigidbody.velocity = Vector3.zero;
        animationHandler.PlayTargetAnimation("Item Pick Up Animation", true);
        playerInventory.weaponInventory.Add(weapon);
        //using destory for now. will implement object pooling later
        Destroy(gameObject);
    }
    
}

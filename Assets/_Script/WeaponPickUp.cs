using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class WeaponPickUpEvent : UnityEvent<WeaponItem>{}

public class WeaponPickUp : Interactable
{
    public WeaponItem weapon;
    public WeaponPickUpEvent onWeaponPickUp;

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
        PlayerAnimationHandler playerAnimationHandler;
        
        playerInventory = playerManager.GetComponent<PlayerInventory>();
        playerLocomotion = playerManager.GetComponent<PlayerLocalmotion>();
        playerAnimationHandler = playerManager.GetComponentInChildren<PlayerAnimationHandler>();

        playerLocomotion.StopPlayer();
        playerAnimationHandler.PlayTargetAnimation("Item Pick Up Animation", true);
        playerInventory.weaponInventory.Add(weapon);
        //UI element to show what item player picks up, thinking about replacing this with event system
        onWeaponPickUp.Invoke(weapon);
        gameObject.SetActive(false);
        //using Destory() for now. will implement object pooling later
        //Destroy(gameObject);
    }
    
}

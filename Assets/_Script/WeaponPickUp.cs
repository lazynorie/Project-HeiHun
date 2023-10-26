using System;
using UnityEngine.Events;



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
        PlayerAnimationHandler playerAnimationHandler;
        
        playerInventory = playerManager.GetComponent<PlayerInventory>();
        playerLocomotion = playerManager.GetComponent<PlayerLocalmotion>();
        playerAnimationHandler = playerManager.GetComponentInChildren<PlayerAnimationHandler>();

        playerLocomotion.StopPlayer();
        playerAnimationHandler.PlayTargetAnimation("Item Pick Up Animation", true);
        playerInventory.weaponInventory.Add(weapon);
        //UI element to show what item player picks up, thinking about replacing this with event system
        
        interactableUI.SwitchItemPickUpText(true);
        interactableUI.ShowItemPickUpText(this);
        interactableUI.SetImageForItemPickUp(weapon);
        gameObject.SetActive(false);
        //using Destory() for now. will implement object pooling later
        //Destroy(gameObject);
    }
    
}

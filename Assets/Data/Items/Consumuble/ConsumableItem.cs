using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : Item
{
    public int maxAmount;
    public int currentAmount;

    public GameObject itemModel;

    public string consumableAnimation;
    public bool isInteracting;

    public virtual void AttemptToConsumeItem(PlayerAnimationHandler anim, WeaponSlotManager weaponSlotManager,
        PlayerVFXManager playerVFXManager)
    {
        if (currentAmount > 0)
        {
            anim.PlayTargetAnimation(consumableAnimation, isInteracting, true);            
        }
        else
        {
            Debug.Log("You dont have enough charge.");
            //todo: play animation 
        }
    }


}

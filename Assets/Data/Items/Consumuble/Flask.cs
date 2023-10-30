using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/Consumables/Flasks")]
public class Flask : ConsumableItem
{
    public enum FlaskType
    {
        Healing,
        Mana,
        Both
    }

    public FlaskType flaskType;
    public int healthRestoreAmount;
    public int manaRestoreAmount;

    public GameObject recoveryVFX;

    public override void AttemptToConsumeItem(PlayerAnimationHandler anim, WeaponSlotManager weaponSlotManager,
        PlayerVFXManager playerVFXManager)
    {
        base.AttemptToConsumeItem(anim,weaponSlotManager,playerVFXManager);
        //restore hp and/or mana
        if (recoveryVFX != null)
        {
            playerVFXManager.currentVFX = recoveryVFX;
        }
        else Debug.Log("assign VFX to flask");
        GameObject flask = Instantiate(itemModel, weaponSlotManager.rightHandSlot.transform);
        playerVFXManager.restoringAmount = healthRestoreAmount;
        playerVFXManager.instantiatedVFXmodel = flask;
        //spawn flask model in player hand and play drinking animation
        weaponSlotManager.rightHandSlot.UnloadWeapon();
        //play recoveryVFX when the recovering animation happened
    }
}

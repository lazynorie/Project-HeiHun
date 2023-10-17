using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
  private PlayerAnimationHandler playerAnimationHandler;
  public string lastAttack;
  private InputHandler inputHandler;

  private void Awake()
  {
    playerAnimationHandler = GetComponentInChildren<PlayerAnimationHandler>();
    inputHandler = GetComponent<InputHandler>();
  }

  public void HandleLightAttack(WeaponItem weapon)
  {
    if (inputHandler.twoHandFlag)
    {
      if (weapon.TH_light_attack_01 == "")
      {
        Debug.Log("TH_light_attack_01 animation not assigned");
        return;
      }//null check
      playerAnimationHandler.PlayTargetAnimation(weapon.TH_light_attack_01,true);
    }
    else
    {
      if (weapon.OH_light_attack_01 == "")
      {
        Debug.Log("light attack 01 animation not assigned");
        return;
      }//null check
      playerAnimationHandler.PlayTargetAnimation(weapon.OH_light_attack_01,true);
       lastAttack = weapon.OH_light_attack_01;
       Debug.Log("light attack 01");
    }
  }
  

  public void HandleHeavyAttack(WeaponItem weapon)
  {
    if (inputHandler.twoHandFlag)
    {
      if (weapon.TH_heavy_attack == "")
      {
        Debug.Log("TH_light_attack_01 animation not assigned");
        return;
      }//null check
      playerAnimationHandler.PlayTargetAnimation(weapon.TH_heavy_attack,true);
    }
    else
    {
      if (weapon.OH_heavy_attack == "")
      {
        Debug.Log("HeavyAttack animation not Assigned");
        return;
      }//null check
      playerAnimationHandler.PlayTargetAnimation(weapon.OH_heavy_attack,true);
      lastAttack = weapon.OH_heavy_attack;
    }
  }

  public void HandleWeaponCombo(WeaponItem weapon)
  {
    if (inputHandler.comboFlag)
    {
      playerAnimationHandler.animator.SetBool("canDoCombo", false);
      if (lastAttack == weapon.OH_light_attack_01)
      {
        if (weapon.OH_light_attack_02 == "")
        {
          Debug.Log("light attack 02 animation not assigned");
          return;
        }
        playerAnimationHandler.PlayTargetAnimation(weapon.OH_light_attack_02, true);
        Debug.Log("light attack 02");
      }
    }
  }
}

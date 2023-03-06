using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
  private AnimationHandler animationHandler;
  public string lastAttack;
  private InputHandler inputHandler;

  private void Awake()
  {
    animationHandler = GetComponentInChildren<AnimationHandler>();
    inputHandler = GetComponent<InputHandler>();
  }

  public void HandleLightAttack(WeaponItem weapon)
  {
    if (weapon.OH_light_attack_01 == "")
    {
      Debug.Log("light attack 01 animation not assigned");
      return;
    }
    animationHandler.PlayTargetAnimation(weapon.OH_light_attack_01,true);
    lastAttack = weapon.OH_light_attack_01;
    Debug.Log("light attack 01");
  }
  

  public void HandleHeavyAttack(WeaponItem weapon)
  {
    if (weapon.OH_heavy_attack == "")
    {
      Debug.Log("HeavyAttack animation not Assigned");
      return;
    }
    animationHandler.PlayTargetAnimation(weapon.OH_heavy_attack,true);
    lastAttack = weapon.OH_heavy_attack;
    Debug.Log(weapon.OH_heavy_attack.ToString());
  }

  public void HandleWeaponCombo(WeaponItem weapon)
  {
    if (inputHandler.comboFlag)
    {
      animationHandler.animator.SetBool("canDoCombo", false);
      if (lastAttack == weapon.OH_light_attack_01)
      {
        if (weapon.OH_light_attack_02 == "")
        {
          Debug.Log("light attack 02 animation not assigned");
          return;
        }
        animationHandler.PlayTargetAnimation(weapon.OH_light_attack_02, true);
        Debug.Log("light attack 02");
      }
    }
  }
}

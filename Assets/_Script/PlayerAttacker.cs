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
    animationHandler.PlayTargetAnimation(weapon.OH_light_attack_01,true);
    lastAttack = weapon.OH_light_attack_01;
  }

  public void HandleHeavyAttack(WeaponItem weapon)
  {
    animationHandler.PlayTargetAnimation(weapon.OH_heavy_attack,true);
    lastAttack = weapon.OH_heavy_attack;
  }

  public void HandleWeaponCombo(WeaponItem weapon)
  {
    if (inputHandler.comboFlag)
    {
      animationHandler.animator.SetBool("canDoCombo", false);
      if (lastAttack == weapon.OH_light_attack_01)
      {
        animationHandler.PlayTargetAnimation(weapon.OH_light_attack_02, true);
      }
    }
  }
}

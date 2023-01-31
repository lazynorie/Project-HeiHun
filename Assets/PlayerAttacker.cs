using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
  private AnimationHandler animationHandler;

  private void Awake()
  {
    animationHandler = GetComponentInChildren<AnimationHandler>();
  }

  public void HandleLightAttack(WeaponItem weapon)
  {
    animationHandler.PlayTargetAnimation(weapon.OH_light_attack,true);
  }

  public void HandleHeavyAttack(WeaponItem weapon)
  {
    animationHandler.PlayTargetAnimation(weapon.OH_heavy_attack,true);
  }
}

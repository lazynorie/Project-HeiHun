using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

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
      if (weapon.TH_light_attack_01 == "")//todo: switch to new system
      {
        Debug.Log("TH_light_attack_01 animation not assigned");
        return;
      }//null check
      playerAnimationHandler.PlayTargetAnimation(weapon.TH_light_attack_01,true);
      //playerAnimationHandler.PlayTargetAnimation(weapon.OH_light_attack_1.AttackAnimationName, true);
    }
    else
    {
      if (weapon.OH_light_attack_1 == null)
      {
        Debug.Log("light attack 01 animation not assigned");
        return;
      }//null check
      playerAnimationHandler.PlayTargetAnimation(weapon.OH_light_attack_1.AttackAnimationName, true);

       //lastAttack = weapon.OH_light_attack_01;
       lastAttack = weapon.OH_light_attack_1.AttackAnimationName;
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
      //todo: switch to the new system
      playerAnimationHandler.PlayTargetAnimation(weapon.TH_heavy_attack,true);
      //playerAnimationHandler.PlayTargetAnimation(weapon.OH_heavy_attack_1.AttackAnimationName,true);
    }
    else
    {
      if (weapon.OH_heavy_attack_1 == null)
      {
        Debug.Log("OH HeavyAttack animation not Assigned");
        return;
      }//null check
      playerAnimationHandler.PlayTargetAnimation(weapon.OH_heavy_attack_1.AttackAnimationName,true);
      lastAttack = weapon.OH_heavy_attack_1.AttackAnimationName;
    }
  }

  public void HandleWeaponCombo(WeaponItem weapon)
  {
    if (inputHandler.comboFlag)
    {
      playerAnimationHandler.animator.SetBool("canDoCombo", false);
      if (lastAttack == weapon.OH_light_attack_1.AttackAnimationName)
      {
        if (weapon.OH_light_attack_2 == null)
        {
          Debug.Log("light attack 02 animation not assigned");
          return;
        }
        playerAnimationHandler.PlayTargetAnimation(weapon.OH_light_attack_2.AttackAnimationName, true);
      }
    }
  }

  public void SetIsLeftHandAttack(bool isAttacking)
  {
    playerAnimationHandler.animator.SetBool("isUsingLeftHand" ,isAttacking);
  }

  public void SetIsRightHandAttack(bool isAttacking)
  {
    playerAnimationHandler.animator.SetBool("isUsingRightHand" ,isAttacking);

  }
}

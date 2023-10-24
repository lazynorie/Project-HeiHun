using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class PlayerAttacker : MonoBehaviour
{
  [SerializeField]private PlayerAnimationHandler playerAnimationHandler;
  [SerializeField]private InputHandler inputHandler;
  [SerializeField] private PlayerInventory playerInventory;
  [SerializeField]private PlayerManager playerManager;
  public string lastAttack;


  private void Awake()
  {
    playerAnimationHandler = GetComponent<PlayerAnimationHandler>();
    inputHandler = GetComponentInParent<InputHandler>();
    playerInventory = GetComponentInParent<PlayerInventory>();
    playerManager = GetComponentInParent<PlayerManager>();
  }

  public void HandleLightAttack(WeaponItem weapon)
  {
    if (inputHandler.twoHandFlag)
    {
      if (weapon.TH_heavy_attack == "")//todo: switch to new system
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
      playerAnimationHandler.PlayTargetAnimation(weapon.OH_light_attack_1.attackAnimationName, true);

       //lastAttack = weapon.OH_light_attack_01;
       lastAttack = weapon.OH_light_attack_1.attackAnimationName;
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
      playerAnimationHandler.PlayTargetAnimation(weapon.OH_heavy_attack_1.attackAnimationName,true);
      lastAttack = weapon.OH_heavy_attack_1.attackAnimationName;
    }
  }
  public void HandleWeaponCombo(WeaponItem weapon)
  {
    if (inputHandler.comboFlag)
    {
      playerAnimationHandler.animator.SetBool("canDoCombo", false);
      if (lastAttack == weapon.OH_light_attack_1.attackAnimationName)
      {
        if (weapon.OH_light_attack_2 == null)
        {
          Debug.Log("light attack 02 animation not assigned");
          return;
        }
        playerAnimationHandler.PlayTargetAnimation(weapon.OH_light_attack_2.attackAnimationName, true);
      }
    }
  }

  #region Input Actions
  public void HandleRbAction()
  {
    if (playerInventory.rightWeapon.weaponType is WeaponType.MeleeWeapon)
    {
      //todo: handle melee action
      PerformRbMeleeAction();
    }
    else if (playerInventory.rightWeapon.weaponType is WeaponType.SpellCaster)
    {
      //todo: handle spell casting
      PerformRbMagicAction(playerInventory.rightWeapon);
    }
    else if (playerInventory.rightWeapon.weaponType is WeaponType.RangeWeapon)
    {
      //todo: handle range action
    }
    else if (playerInventory.rightWeapon.weaponType is WeaponType.FaithCaster)
    {
      //todo: handle Miracle action
    }
    else if (playerInventory.rightWeapon.weaponType is WeaponType.PyroCaster)
    {
      //todo: handle fire magic action
    }
  }
  public void HandleRtAction()
  {
    
  }

  #endregion

  #region Attack Actions
  public void PerformRbMeleeAction()
  {
      if (playerManager.canDoCombo)
      {
        inputHandler.comboFlag = true;
        HandleWeaponCombo(playerInventory.rightWeapon);
        inputHandler.comboFlag = false;
      }
      else
      {
        if (playerManager.isInteracting)
          return;
        if (playerManager.canDoCombo)
          return;
        HandleLightAttack(playerInventory.rightWeapon);
      }
  }
  public void PerformRbMagicAction(WeaponItem weapon)
  {
    if (weapon.weaponType is WeaponType.FaithCaster)
    {
      if (playerInventory.currentSpell != null && playerInventory.currentSpell.spellType is SpellType.Faith)
      {
        //check for mana
        //attempt to cast 
      }
    }
  }
  #endregion
  public void SetIsLeftHandAttack(bool isAttacking)
  {
    playerAnimationHandler.animator.SetBool("isUsingLeftHand" ,isAttacking);
  }
  public void SetIsRightHandAttack(bool isAttacking)
  {
    playerAnimationHandler.animator.SetBool("isUsingRightHand" ,isAttacking);

  }
}

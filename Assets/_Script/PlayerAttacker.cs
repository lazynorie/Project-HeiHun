using System;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.MeshOperations;

public class PlayerAttacker : MonoBehaviour
{
  [SerializeField] private PlayerAnimationHandler playerAnimationHandler;
  [SerializeField] private InputHandler inputHandler;
  [SerializeField] private PlayerInventory playerInventory;
  [SerializeField] private PlayerManager playerManager;
  [SerializeField] private PlayerStats playerStats;
  public string lastAttack;
  private bool hasEnoughMana;
  private bool hasEnoughStamina;
  [SerializeField] private LayerMask backStabLayer;


  private void Awake()
  {
    playerAnimationHandler = GetComponent<PlayerAnimationHandler>();
    inputHandler = GetComponentInParent<InputHandler>();
    playerInventory = GetComponentInParent<PlayerInventory>();
    playerManager = GetComponentInParent<PlayerManager>();
    playerStats = GetComponentInParent<PlayerStats>();
  }

  private void Start()
  {
    SpellItem.OnAttemptToCastSpell += CheckIfPlayerHasEnoughMana;
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
      if (weapon.ohLightActionAttack1 == null)
      {
        Debug.Log("ohLightActionAttack1 is not assigned");
        return;
      }//null check
      playerAnimationHandler.PlayTargetAnimation(weapon.ohLightActionAttack1.animationName, true);

       //lastAttack = weapon.OH_light_attack_01;
       lastAttack = weapon.ohLightActionAttack1.animationName;
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
      if (weapon.ohHeavyActionAttack1 == null)
      {
        Debug.Log("ohHeavyActionAttack1 not Assigned");
        return;
      }//null check
      playerAnimationHandler.PlayTargetAnimation(weapon.ohHeavyActionAttack1.animationName,true);
      lastAttack = weapon.ohHeavyActionAttack1.animationName;
    }
  }
  public void HandleWeaponCombo(WeaponItem weapon)
  {
    if (inputHandler.comboFlag)
    {
      playerAnimationHandler.animator.SetBool("canDoCombo", false);
      if (lastAttack == weapon.ohLightActionAttack1.animationName)
      {
        if (weapon.ohLightActionAttack2 == null)
        {
          Debug.Log("ohLightActionAttack2 not assigned");
          return;
        }
        playerAnimationHandler.PlayTargetAnimation(weapon.ohLightActionAttack2.animationName, true);
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
    else if (playerInventory.rightWeapon.weaponType is WeaponType.SpellCaster ||
             playerInventory.rightWeapon.weaponType is WeaponType.FaithCaster ||
             playerInventory.rightWeapon.weaponType is WeaponType.PyroCaster)
    {
      //handle spell faith and fire magic
      PerformRbMagicAction(playerInventory.rightWeapon);
    }
    else if (playerInventory.rightWeapon.weaponType is WeaponType.RangeWeapon)
    {
      //todo: handle range action
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
    if (playerManager.isInteracting)
      return;
    if (weapon.weaponType is WeaponType.FaithCaster)
    {
      if (playerInventory.currentSpell != null && playerInventory.currentSpell.spellType is SpellType.Faith)
      {
        //check for mana
        if (playerStats.currentMana >= playerInventory.currentSpell.manaCost)
        {
          playerInventory.currentSpell.AttempToCastSpell(playerAnimationHandler,playerStats);
        }
        else Debug.Log("not enough mana");//todo: player a oh i fucked up animation 
      }
    }
  }

  private void SuccessfullyCastSpell()
  {
    //todo: check mana here if you want the casting animation to go through
    playerInventory.currentSpell.SuccessfulCastSpell(playerAnimationHandler,playerStats);
  }
  #endregion
  public void CheckIfPlayerHasEnoughMana(int requiredMana)
  {
    hasEnoughMana = playerStats.currentMana > requiredMana;
    Debug.Log("current mana " + hasEnoughMana);
  }

  public void AttemptBackStabOrRiposte()
  {
    RaycastHit hit;
    if (Physics.Raycast(inputHandler.criticalAttackRaycastStartPoint.position,
          transform.TransformDirection(Vector3.forward),out hit, 0.5f, backStabLayer))
    {
      CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
      if (enemyCharacterManager != null)
      {
        //check for team ID
        //pull into a transform.positon so that the animation doesnt look off
        playerManager.transform.position = enemyCharacterManager.backStabCollider.backStabberPoint.position;
        //rotate towards target transform
        Vector3 rotationDir = playerManager.transform.root.eulerAngles;
        rotationDir = hit.transform.position - playerManager.transform.position;
        rotationDir.y = 0;
        rotationDir.Normalize();
        Quaternion tr = Quaternion.LookRotation(rotationDir);
        Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
        playerManager.transform.rotation = targetRotation;
        
        //play animation
        playerAnimationHandler.PlayTargetAnimation("Stab",true);
        enemyCharacterManager.GetComponentInChildren<AnimationHandler>().PlayTargetAnimation("Stabbed",true);
        //enemy play animation
        //do damage
      }
    }
  }
}

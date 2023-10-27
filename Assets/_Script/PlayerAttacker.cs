using System;
using System.ComponentModel.Design;
using Unity.VisualScripting;
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
  [SerializeField] private PlayerLocalmotion playerLocoMotion;
  private WeaponSlotManager weaponSlotManager;
  public ActionAttack lastAttack;
  private bool hasEnoughMana;
  private bool hasEnoughStamina;
  [SerializeField] private LayerMask backStabLayer;
  [SerializeField] private LayerMask riposteLayer;

  public GameObject hitObject;

  private void Awake()
  {
    playerAnimationHandler = GetComponent<PlayerAnimationHandler>();
    inputHandler = GetComponentInParent<InputHandler>();
    playerInventory = GetComponentInParent<PlayerInventory>();
    playerManager = GetComponentInParent<PlayerManager>();
    playerStats = GetComponentInParent<PlayerStats>();
    weaponSlotManager = GetComponent<WeaponSlotManager>();
    playerLocoMotion = GetComponentInParent<PlayerLocalmotion>();
  }

  private void Start()
  {
    SpellItem.OnAttemptToCastSpell += CheckIfPlayerHasEnoughMana;
  }
  
  private void HandleLightAttack(WeaponItem weapon)
  {
    if (playerStats.currentStamina <= 0) return;
    if (inputHandler.twoHandFlag)
    {
      if (weapon.TH_light_attack_01 == null)//
      {
        Debug.Log("TH_light_attack_01 animation not assigned");
        return;
      }//null check
      playerAnimationHandler.PlayTargetAnimation(weapon.TH_light_attack_01.animationName,true);
      lastAttack = weapon.TH_light_attack_01;
    }//two hand light attack
    else//one hand light attack
    {
      if (weapon.ohLightActionAttack1 == null)
      {
        Debug.Log("ohLightActionAttack1 is not assigned");
        return;
      }
      playerAnimationHandler.PlayTargetAnimation(weapon.ohLightActionAttack1.animationName, true);
      lastAttack = weapon.ohLightActionAttack1;
    }
  }
  private void HandleHeavyAttack(WeaponItem weapon)
  {
    if (playerStats.currentStamina <= 0) return;
    if (inputHandler.twoHandFlag)//handle two hand heavy attack
    {
      if (weapon.TH_heavy_attack_01 == null)
      {
        Debug.Log("TH_heavy_attack_01 not assigned");
        return;
      }
      playerAnimationHandler.PlayTargetAnimation(weapon.TH_heavy_attack_01.animationName,true);
      lastAttack = weapon.TH_heavy_attack_01;
    }
    else//handle one hand heavy attack
    {
      if (weapon.ohHeavyActionAttack1 == null)
      {
        Debug.Log("ohHeavyActionAttack1 not Assigned");
        return;
      }//null check
      playerAnimationHandler.PlayTargetAnimation(weapon.ohHeavyActionAttack1.animationName,true);
      lastAttack = weapon.ohHeavyActionAttack1;
    }
  }
  private void HandleWeaponCombo(WeaponItem weapon)
  {
    if (playerStats.currentStamina <= 0) return;
    if (inputHandler.comboFlag)
    {
      playerAnimationHandler.animator.SetBool("canDoCombo", false);
      if (lastAttack == weapon.ohLightActionAttack1)
      {
        if (weapon.ohLightActionAttack2 == null)
        {
          Debug.Log("ohLightActionAttack2 not assigned");
          return;
        }
        playerAnimationHandler.PlayTargetAnimation(weapon.ohLightActionAttack2.animationName, true);
      }
      else if (lastAttack == weapon.TH_heavy_attack_01)
      {
        playerAnimationHandler.PlayTargetAnimation(weapon.TH_heavy_attack_02.animationName, true);
      }
      else if (lastAttack == weapon.TH_light_attack_01)
      {
        playerAnimationHandler.PlayTargetAnimation(weapon.TH_light_attack_02.animationName, true);
      }
      
    }
  }

  #region Input Actions
  public void HandleRbAction()
  {
    if (playerInventory.rightWeapon.weaponType is WeaponType.MeleeWeapon)
    {
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
    if (playerInventory.rightWeapon.weaponType is WeaponType.MeleeWeapon)
    {
      PerformRtMeleeAction();
    }
  }

  #endregion

  #region Attack Actions

  private void PerformRbMeleeAction()
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

  private void PerformRtMeleeAction()
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
      HandleHeavyAttack(playerInventory.rightWeapon);
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
    if (playerStats.currentStamina <= 0) return;//check stamina
    if (playerInventory.rightWeapon.weaponType != WeaponType.MeleeWeapon)
    {
      Debug.Log("You are not holding a melee weapon");
      return;
    }// check weapon type for backstab
    RaycastHit hit;
    if (Physics.Raycast(inputHandler.criticalAttackRaycastStartPoint.position,
          transform.TransformDirection(Vector3.forward),out hit, 0.5f, backStabLayer))
    {
      Debug.DrawLine(inputHandler.criticalAttackRaycastStartPoint.position,hit.point,Color.red,2);
      CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
      DamageCollider rightWeapon = weaponSlotManager.rightHandDamageCollider;
      if (enemyCharacterManager != null)
      {
        if (enemyCharacterManager.GetComponent<EnemyStats>().isDead)
        {
          return;
        }
        //todo: check for team ID
        //pull into a transform.positon so that the animation doesnt look off
        playerManager.transform.position = Vector3.Lerp(playerManager.transform.position,enemyCharacterManager.criticalDamageColliders[0].criticalDamageTransformPoint.position,1/Time.deltaTime);
        //rotate towards target transform
        playerLocoMotion.RotateTowardsTarget(hit.transform, 500);
        /*Vector3 rotationDir = playerManager.transform.root.eulerAngles;
        rotationDir = hit.transform.position - playerManager.transform.position;
        rotationDir.y = 0;
        rotationDir.Normalize();
        Quaternion tr = Quaternion.LookRotation(rotationDir);
        Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
        playerManager.transform.rotation = targetRotation;*/
        //do damage
        int criticalDamage = playerInventory.rightWeapon.criticalDamageMuiliplier *
                             rightWeapon.weapondamage;
        enemyCharacterManager.GetComponent<EnemyStats>().pendingCriticalDamage = criticalDamage;
        playerAnimationHandler.PlayTargetAnimation("Stab",true);//play animation
        enemyCharacterManager.GetComponentInChildren<AnimationHandler>().PlayTargetAnimation("Stabbed",true);//enemy play animation
      }
    }
    else if (Physics.Raycast(inputHandler.criticalAttackRaycastStartPoint.position,
                  transform.TransformDirection(Vector3.forward),out hit, 0.5f, riposteLayer))
    {
      //todo: check if player hitting themselves
      CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
      DamageCollider rightWeapon = weaponSlotManager.rightHandDamageCollider;
      Debug.DrawLine(inputHandler.criticalAttackRaycastStartPoint.position,hit.point,Color.red,2);
      if (enemyCharacterManager.GetComponent<EnemyStats>().isDead) return;
      if (enemyCharacterManager != null && enemyCharacterManager.canBeRiposted)
      {
        playerManager.transform.position =
          enemyCharacterManager.criticalDamageColliders[1].criticalDamageTransformPoint.position;
        playerLocoMotion.RotateTowardsTarget(hit.transform, 500);
        int criticalDamage = playerInventory.rightWeapon.criticalDamageMuiliplier *
                             rightWeapon.weapondamage;
        enemyCharacterManager.GetComponent<EnemyStats>().pendingCriticalDamage = criticalDamage;
        playerAnimationHandler.PlayTargetAnimation("Stab",true);//play animation
        enemyCharacterManager.GetComponentInChildren<AnimationHandler>().PlayTargetAnimation("CriticalAttackFront",true);//enemy play animation
      }
    }
  }
  
}



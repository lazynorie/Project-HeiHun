using System;
using UnityEngine;

public class PlayerStats : CharacterStats
{
   public static event Action onPlayerDeath;
   private PlayerManager playerManager;
   public HealthBar healthbar;
   public StaminaBar staminaBar;
   private PlayerAnimationHandler animhandler;
   [SerializeField]
   private int experience;
   public int tili;
   public int Experience
   {
      get
      {
         return experience;
      }
      set
      {
         experience = value;
      }
   }
   [SerializeField]
   private int level;
   public int Level {
      get
      {
         return experience / 1000;
      }
      set
      {
         level = value;
      }
   }

   private void Awake()
   {
      playerManager = GetComponent<PlayerManager>();
   }

   private void Start()
   {
      maxHealth = SetMaxHealthFromHealthLevel();
      maxStamina = SetMaxStaminaFromStaminaLevel();
      currentHealth = maxHealth;
      currentStamina = maxStamina;
      healthbar.SetMaxHealth(maxHealth);
      staminaBar.SetMaxStamina(maxStamina);
      animhandler = GetComponentInChildren<PlayerAnimationHandler>();
      EnemyStats.onEnemyDeath += IncreasePlayerExperience;
   }
   
   private void Update()
   {
      RegenStamina();
   }

   private float SetMaxStaminaFromStaminaLevel()
   {
      maxStamina = staminaLevel * 5 + 10;
      return maxStamina;
   }

   private int SetMaxHealthFromHealthLevel()
   {
      //在这里设置玩家血量
      maxHealth = healthLevel * 10;
      return maxHealth;
   }

   public void TakeDamage(int damage)
   {
      if (isDead) return;
      if (!playerManager.isInvulnerable)
      {
         currentHealth = currentHealth - damage;
      
         healthbar.SetCurrentHealth(currentHealth);
      
         animhandler.PlayTargetAnimation("Getting Hit", true);
      }

      if (currentHealth <=0)
      {
         currentHealth = 0;
         animhandler.PlayTargetAnimation("dead01", true);
         onPlayerDeath?.Invoke();
         //玩家死亡逻辑
      }
   }
   public void DrainStamina(int staminaCost)
   {
      if (currentStamina <= 0) return;
      currentStamina -= staminaCost;
      staminaBar.SetCurrentStamina(currentStamina);
   }
   public bool CheckIfEnoughStamina()
   {
      //todo: use this to check if player have enough stamina to perform the action
      return true;
   }
   private void IncreasePlayerExperience(int exp)
   {
      experience += exp;
   }

   private void RegenStamina()
   {
      if (!playerManager.isInteracting)
      {
         if (currentStamina < maxStamina)
         {
            currentStamina += staminaRegenRate * Time.deltaTime;
            staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));//need to update every frame
         }
      }
   }
}


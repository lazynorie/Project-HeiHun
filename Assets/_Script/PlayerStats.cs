using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStats : CharacterStats
{
   public static event Action<int> OnSolUpdate;
   public static event Action OnPlayerDeath;
   private PlayerManager playerManager;
   public HealthBar healthBar;
   public StaminaBar staminaBar;
   public ManaBar manaBar;
   private PlayerAnimationHandler animHandler;
   [FormerlySerializedAs("experience")] [SerializeField]
   private int solCount;

   public int SolCount
   {
      get
      {
         return solCount;
      }
      set
      {
         solCount = value;
      }
   }
   [SerializeField]
   private int level;

   [Header("Player Stamina RegenTimer")]
   [SerializeField]
   private float staminaRegenRateTimer;

   private void Awake()
   {
      playerManager = GetComponent<PlayerManager>();
      animHandler = GetComponentInChildren<PlayerAnimationHandler>();
      healthBar = FindObjectOfType<HealthBar>();
      staminaBar = FindObjectOfType<StaminaBar>();
      manaBar = FindObjectOfType<ManaBar>();
   }
   private void Start()
   {
      SetUpPlayerStats();
      EnemyStats.OnEnemyDeath += IncreasePlayerSolCount;
      HealingSpell.OnHealingSpellCast += HealPlayer;
      SpellItem.OnSpellSuccessfullyCast += DrainMana;
   }

   private void SetUpPlayerStats()
   {
      maxMana = SetMaxManaFromManaLevel();
      maxHealth = SetMaxHealthFromHealthLevel();
      maxStamina = SetMaxStaminaFromStaminaLevel();
      currentHealth = maxHealth;
      currentStamina = maxStamina;
      currentMana = maxMana;
      healthBar.SetMaxHealth(maxHealth);
      staminaBar.SetMaxStamina(maxStamina);
      manaBar.SetMaxMana(maxMana);
   }
   private void Update()
   {
      RegenStamina();
   }
   private int SetMaxStaminaFromStaminaLevel()
   {
      maxStamina = staminaLevel * 5 + 10;
      return Mathf.RoundToInt(maxStamina);
   }
   private int SetMaxHealthFromHealthLevel()
   {
      //在这里设置玩家血量
      maxHealth = healthLevel * 10;
      return maxHealth;
   }
   private int SetMaxManaFromManaLevel()
   {
      maxMana = manaLevel * 5 + 50;
      return maxMana;
   }
   public void TakeDamage(int damage, string animation = "Getting Hit")
   {
      if (isDead) return;
      if (!playerManager.isInvulnerable)
      {
         currentHealth = currentHealth - damage;
      
         healthBar.SetCurrentHealth(currentHealth);
      
         animHandler.PlayTargetAnimation(animation, true);
      }

      if (currentHealth <=0)
      {
         currentHealth = 0;
         isDead = true;
         animHandler.PlayTargetAnimation("dead01", true);
         OnPlayerDeath?.Invoke();
         //玩家死亡逻辑
      }
   }

   public void TakeDamageWithOutAnimation(int damage)
   {
      if (isDead) return;
      if (!playerManager.isInvulnerable)
      {
         currentHealth = currentHealth - damage;
         healthBar.SetCurrentHealth(currentHealth);
      }

      if (currentHealth <=0)
      {
         currentHealth = 0;
         OnPlayerDeath?.Invoke();
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
   private void IncreasePlayerSolCount(int sol)
   {
      solCount += sol;
      OnSolUpdate?.Invoke(solCount);
   }
   private void RegenStamina()
   {
      if (playerManager.isInteracting)
      {
         staminaRegenRateTimer = 0;
      }
      else
      {
         staminaRegenRateTimer += Time.deltaTime;
         if (currentStamina < maxStamina && staminaRegenRateTimer > 1f)
         {
            currentStamina += staminaRegenRate * Time.deltaTime;
            staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));//need to update every frame
         }
      }
   }
   private void HealPlayer(int healAmount)
   {
      if (currentHealth < maxHealth)
      {
         currentHealth += healAmount;
      }
      healthBar.SetCurrentHealth(currentHealth);
   }
   public void HealPlayerOverTime(float healrate, float lastTime)
   {
      lastTime -= Time.deltaTime;
      if (lastTime > 0)
      {
         currentHealth += (int)(healrate * Time.deltaTime);
      }
      
      if (currentHealth > maxHealth)
      {
         currentHealth = maxHealth;
      }
      healthBar.SetCurrentHealth(currentHealth);
   }
   public void DrainMana(int manaCost)
   {
      currentMana -= manaCost;
      if (currentMana < 0) currentMana = 0;
      manaBar.SetCurrentMana(currentMana);
   }
   
}


using System;
using UnityEngine;

public class PlayerStats : CharacterStats
{
   public static event Action onPlayerDeath;
   public HealthBar healthbar;
   private PlayerAnimationHandler animhandler;
   [SerializeField]
   private int experience;
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

   private void Start()
   {
      maxHealth = SetMaxHealthFromHealthLevel();
      currentHealth = maxHealth;
      healthbar.SetMaxHealth(maxHealth);
      animhandler = GetComponentInChildren<PlayerAnimationHandler>();
      EnemyStats.onEnemyDeath += IncreasePlayerExperience;
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
      currentHealth = currentHealth - damage;
      
      healthbar.SetCurrentHealth(currentHealth);
      
      animhandler.PlayTargetAnimation("Getting Hit", true);

      if (currentHealth <=0)
      {
         currentHealth = 0;
         animhandler.PlayTargetAnimation("dead01", true);
         onPlayerDeath?.Invoke();
         //玩家死亡逻辑
      }
   }

   private void IncreasePlayerExperience(int exp)
   {
      experience += exp;
   }
}

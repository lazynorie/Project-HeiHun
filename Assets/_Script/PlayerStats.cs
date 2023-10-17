using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
   public HealthBar healthbar;
   private PlayerAnimationHandler animhandler;
   private void Start()
   {
      maxHealth = SetMaxHealthFromHealthLevel();
      currentHealth = maxHealth;
      healthbar.SetMaxHealth(maxHealth);
      animhandler = GetComponentInChildren<PlayerAnimationHandler>();
   }

   private int SetMaxHealthFromHealthLevel()
   {
      //在这里设置玩家血量
      maxHealth = healthLevel * 10;
      return maxHealth;
   }

   public void TakeDamage(int damage)
   {
      currentHealth = currentHealth - damage;
      
      healthbar.SetCurrentHealth(currentHealth);
      
      animhandler.PlayTargetAnimation("Getting Hit", true);

      if (currentHealth <=0)
      {
         currentHealth = 0;
         animhandler.PlayTargetAnimation("dead01", true);
         //玩家死亡逻辑
      }
   }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
   public int healthLevel = 10;
   public int maxHealth;
   public int currentHealth;

   public HealthBar healthbar;
   private AnimationHandler animhandler;
   private void Start()
   {
      maxHealth = SetMaxHealthFromHealthLevel();
      currentHealth = maxHealth;
      healthbar.SetMaxHealth(maxHealth);
      animhandler = GetComponentInChildren<AnimationHandler>();
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

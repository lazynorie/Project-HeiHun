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
   private void Start()
   {
      maxHealth = SetMaxHealthFromHealthLevel();
      currentHealth = maxHealth;
      healthbar.SetCurrentHealth(maxHealth);
   }

   private int SetMaxHealthFromHealthLevel()
   {
      maxHealth = healthLevel * 10;
      return maxHealth;
   }

   public void TakeDamage(int damage)
   {
      currentHealth = currentHealth - damage;
      
      healthbar.SetCurrentHealth(currentHealth);
   }
}

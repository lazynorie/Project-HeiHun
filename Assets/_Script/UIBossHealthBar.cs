using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIBossHealthBar : MonoBehaviour
{
   public TextMeshProUGUI bossName;
   private Slider slider;
   private void Awake()
   {
      slider = GetComponentInChildren<Slider>();
   }

   private void Start()
   {
      DisableBossHealthBar();
      EventColliderForBossFight.OnBossActivate += SetBossName;
      EventColliderForBossFight.OnBossActivate += ActiveBossHealthBar;
      EventColliderForBossFight.OnBossActivate += SetBossMaxHealth;
   }

   private void ActiveBossHealthBar(BossManager obj)
   {
      slider.gameObject.SetActive(true);
   }

   private void SetBossName(BossManager obj)
   {
      bossName.text = obj.bossName;
   }


   public void SetBossName(string name)
   {
      bossName.text = name;
   }

   public void ActiveBossHealthBar()
   {
      slider.gameObject.SetActive(true);
   }

   public void DisableBossHealthBar()
   {
      slider.gameObject.SetActive(false);
   }

   public void SetBossMaxHealth(int maxHealth)
   {
      slider.maxValue = maxHealth;
      slider.value = maxHealth;
   }

   public void SetBossMaxHealth(BossManager obj)
   {
      EnemyStats bossStats = obj.GetComponent<EnemyStats>();
      slider.maxValue = bossStats.maxHealth;
      slider.value = bossStats.maxHealth;
   }

   public void SetBossCurrentHealth(int currentHealth)
   {
      slider.value = currentHealth;
   }

   public void SetBossCurrentHealth(BossManager obj)
   {
      EnemyStats bossStats = obj.GetComponent<EnemyStats>();
      slider.value = bossStats.currentHealth;
   }
}

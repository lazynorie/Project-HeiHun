using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
   public List<FogWall> fogWalls;
   public UIBossHealthBar bossHealthBar;
   public BossManager boss;

   public bool bossFightIsAcitive;
   public bool bossHasBeenAwakened;
   public bool bossHasBeenDefeated;

   private void Awake()
   {
      bossHealthBar = FindObjectOfType<UIBossHealthBar>();
   }

   #region World Event
   public void ActivateBossFight()
   {
      bossFightIsAcitive = true;
      bossHasBeenAwakened = true;

      foreach (var fogWall in fogWalls)
      {
         fogWall.EnableFogWall();
      }
   }

   public void BossHasBeenDefeated()
   {
      bossHasBeenDefeated = true;
      bossFightIsAcitive = false;
      
      foreach (var fogWall in fogWalls)
      {
         fogWall.DisableFogWall();
      }
   }
   #endregion
   
}

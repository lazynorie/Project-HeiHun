using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SolCountUI : MonoBehaviour
{
   [SerializeField]
   private TextMeshProUGUI solCountText;
   private PlayerStats playerStats;
   private void Awake()
   {
       playerStats = FindObjectOfType<PlayerStats>();
   }

   private void Start()
   {
       PlayerStats.OnSolUpdate += UpdateSolCount;
       solCountText.text = playerStats.SolCount.ToString();
   }

   private void UpdateSolCount(int solText)
   {
       solCountText.text = solText.ToString();
   }
}

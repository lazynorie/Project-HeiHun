using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : Item
{
    [Header("Defense Stats")] 
    public float phyDefense;
    //magic defense
    private PlayerStats playerStats;
    

    public void AddStats(PlayerStats playerStats)
    {
        //todo: AddStats
    }

    public void SubtractStats(PlayerStats playerStats)
    {
        //todo: SubtractStats
    }
}

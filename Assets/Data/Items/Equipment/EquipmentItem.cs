using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : Item
{
    [Header("Stats")] 
    public float phyDefense;

    public float healthLevel;

    public float manaLevel;
    
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

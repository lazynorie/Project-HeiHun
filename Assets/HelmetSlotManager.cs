using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetSlotManager : MonoBehaviour
{
    public List<GameObject> helmetModels;
   
    private PlayerStats playerStats;
    private PlayerInventory playerInventory;
    private void Awake()
    {
        GetAllHelmetModels();
        playerStats = GetComponentInParent<PlayerStats>();
        playerInventory = GetComponentInParent<PlayerInventory>();
    }

    public void UnequipAllHelmetModels()
    {
        foreach (var helmetModel in helmetModels)
        {
            helmetModel.SetActive(false);
            //subtract stats to characterStats
            playerInventory.currentHelmet.SubtractStats(playerStats);
        }
    }

    public void EquipHelmetModelByName(string name)
    {
        foreach (var helmetModel in helmetModels)
        {
            if (helmetModel.name == name)
            {
                helmetModel.SetActive(true);
                //add stats to characterStats
                playerInventory.currentHelmet.AddStats(playerStats);
            }
        }
    }
    private void GetAllHelmetModels()
    {
        int childrenGameObjects = transform.childCount;
        for (int i = 0; i < childrenGameObjects; i++)
        {
            helmetModels.Add(transform.GetChild(i).gameObject);
        }
    }
}

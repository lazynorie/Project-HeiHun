using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSlotManager : MonoBehaviour
{
    public List<GameObject> ringModels;
    [Tooltip("This bool checks if its a left hand ring slot or not.")]
    public bool isLeft;
    private PlayerStats playerStats;
    private PlayerInventory playerInventory;
    private void Awake()
    {
        GetAllHelmetModels();
        playerStats = GetComponentInParent<PlayerStats>();
        playerInventory = GetComponentInParent<PlayerInventory>();
    }

    public void UnequipAllRingsModels()
    {
        foreach (var helmetModel in ringModels)
        {
            helmetModel.SetActive(false);
            //subtract stats to characterStats
            playerInventory.currentHelmet.SubtractStats(playerStats);
        }
    }

    public void EquipRingModelByName(string name)
    {
        foreach (var ringModel in ringModels)
        {
            if (ringModel.name == name)
            {
                ringModel.SetActive(true);
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
            ringModels.Add(transform.GetChild(i).gameObject);
        }
    }
}



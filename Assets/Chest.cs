using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    private Animator animator;
    private Chest chest;
    [SerializeField] Transform openCheckPosition;

    [SerializeField] private GameObject itemSpawner;
    public WeaponItem itemInChest;

    private void Awake()
    {
        base.Awake();
        animator = GetComponentInChildren<Animator>();
        chest = GetComponent<Chest>();
    }

    public override void Interact(PlayerManager playerManager)
    {
        //rotate player towards player
        //lock player transform infront of the chest
        //open chest lid
        //spawn item in the chest
        PlayerLocalmotion playerLocalmotion = playerManager.GetComponent<PlayerLocalmotion>();
        playerLocalmotion.RotateTowardsTarget(transform);
        playerLocalmotion.StopPlayer();
        //playerManager.transform.position = openCheckPosition.transform.position;
        playerManager.transform.position = Vector3.Lerp(playerManager.transform.position,openCheckPosition.transform.position,1/Time.fixedDeltaTime);
        animator.Play("Chest Open");
        interactableUI.DisableItemPopUpFrame();

        StartCoroutine(SpawnItemInChest());
        
        WeaponPickUp weaponPickUp = itemSpawner.GetComponent<WeaponPickUp>();
        weaponPickUp.weapon = itemInChest;
    }

   

    private IEnumerator SpawnItemInChest()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(itemSpawner, transform);
        Destroy(chest);
    }
}

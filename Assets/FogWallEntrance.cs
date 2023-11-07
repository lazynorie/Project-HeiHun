using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class FogWallEntrance : Interactable
{
    private GameInstance gameInstance;
    [SerializeField] private EventColliderForBossFight eventColliderForBossFight;
    protected override void Awake()
    {
        base.Awake();
        gameInstance = FindObjectOfType<GameInstance>();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void Interact(PlayerManager playerManager)
    {
        base.Interact(playerManager);
        playerManager.EnterFogWallInteraction(transform);
        eventColliderForBossFight.bossTriggerCollider.isTrigger = true;
        //interactiveCollider.enabled = false;
        //Destroy(this);
    }
}

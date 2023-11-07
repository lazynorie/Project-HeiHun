using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class EventColliderForBossFight : MonoBehaviour
{
    [SerializeField]
    private BossManager bossManager;
    public static event Action<BossManager> OnBossActivate;
    private FogWall fogWall;
    public BoxCollider bossTriggerCollider;

    private void Awake()
    {
        fogWall = GetComponentInParent<FogWall>();
        bossTriggerCollider = GetComponent<BoxCollider>();
        bossTriggerCollider.isTrigger = false;
    }

    public void EnableBossEventCollider()
    {
        gameObject.SetActive(true);
    }
    public void DisableBossEventCollider()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bossManager.isActivated = true;
            OnBossActivate?.Invoke(bossManager);
            fogWall.fogWallCollider.enabled = true;
            Destroy(this);
        }
    }
}

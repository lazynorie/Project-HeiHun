using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWallScript : MonoBehaviour
{
    [SerializeField]
    private BossManager bossManager;
    public static event Action<BossManager> OnBossActivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bossManager.isActivated = true;
            OnBossActivate?.Invoke(bossManager);
            Destroy(this);
        }
    }
}

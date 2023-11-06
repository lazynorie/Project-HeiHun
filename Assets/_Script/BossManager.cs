using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [Tooltip("This is the name of the BOSS")]
    public string bossName;

    private EnemyStats enemyStats;
    private UIBossHealthBar bossHealthBar;
    [SerializeField] private GameObject fogWall;
    public bool isActivated = false;
    private void Awake()
    {
        bossHealthBar = FindObjectOfType<UIBossHealthBar>();
        enemyStats = GetComponent<EnemyStats>();
    }
    
}

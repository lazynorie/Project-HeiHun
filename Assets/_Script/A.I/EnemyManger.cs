using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManger : CharacterManager
{
    private EnemyLocomotionManager enemyLocomotionManager;
    private bool isPerformingAction;
    [Header("A.I")]
    public float detectionRadius = 20f;
    [Header("detection angles")]
    [SerializeField] public float minimumDetectionAngle = -50f;
    [SerializeField] public float maximumDetectionAngle = 50f;

    private void Awake()
    {
        enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
    }
    private void Update()
    {
        HandleCurrentAction();
    }

    private void HandleCurrentAction()
    {
        if (enemyLocomotionManager.currentTarget == null)
        {
            enemyLocomotionManager.HandleDetection();
        }
    }
}

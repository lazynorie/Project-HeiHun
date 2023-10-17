using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : CharacterManager
{
    private EnemyLocomotionManager enemyLocomotionManager;
    private NavMeshAgent navMeshAgent;
    public bool isPerformingAction;
    [Header("A.I")]
    public float detectionRadius = 15f;
    [Header("detection angles")]
    [SerializeField] public float minimumDetectionAngle = -50f;
    [SerializeField] public float maximumDetectionAngle = 50f;

    private void Awake()
    {
        enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
    }
    private void Update()
    {
    }

    private void FixedUpdate()
    {
        HandleCurrentAction();
    }

    private void HandleCurrentAction()
    {
        if (enemyLocomotionManager.currentTarget == null)
        {
            enemyLocomotionManager.HandleDetection();
        }
        else
        {
            enemyLocomotionManager.HandleMoveToTarget();
        }
    }
}

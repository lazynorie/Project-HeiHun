using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbushState : State
{
    public bool isSleeping;
    public float detectionRadius = 2;
    [SerializeField]
    private string sleepingAnimation;
    [SerializeField]
    private string wakeAnimation;

    [SerializeField]
    private LayerMask detectionLayer;
    private void Start()
    {
        AssignStateMachineManager();
        isSleeping = true;
    }

    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimationHandler animationHandler)
    {
        if (isSleeping && enemyManager.isInteracting ==  false)
        {
            animationHandler.PlayTargetAnimation(sleepingAnimation,true);
        }

        Collider[] colliders = Physics.OverlapSphere(enemyManager.transform.position, detectionRadius, detectionLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();
            if (characterStats != null && characterStats.transform.root != transform.root)
            {
                Vector3 targetDir = characterStats.transform.position - enemyManager.transform.position;
                float viewableAngle = Vector3.Angle(targetDir, enemyManager.transform.position);

                if (viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                {
                    enemyManager.currentTarget = characterStats;
                    isSleeping = false;
                    animationHandler.PlayTargetAnimation(wakeAnimation,true);
                }
            }
        }

        if (enemyManager.currentTarget != null)
        {
            return stateMachineManager.pursueTargetState;
        }
        else return this;
    }
}


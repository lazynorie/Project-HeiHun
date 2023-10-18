using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public LayerMask detectionLayer;
    private PursueTargetState pursueTargetState;

    private void Awake()
    {
        AssignStateMachineManager();
        pursueTargetState = stateMachineManager.pursueTargetState;
    }
    public override State Tick(EnemyManager manager, EnemyStats stats, EnemyAnimationHandler anim)
    {
        #region look for potential target
        Collider[] colliders = Physics.OverlapSphere(transform.position, manager.detectionRadius, detectionLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();
            if (characterStats != null)
            {
                //todo: check team ID
                //Vector3 relativeDir = transform.TransformDirection(characterStats.transform.position);
                Vector3 targetDirection = characterStats.transform.position - transform.position;
                float viewbleAngle = Vector3.Angle(targetDirection, transform.forward);
                if (viewbleAngle >manager.minimumDetectionAngle && viewbleAngle <manager.maximumDetectionAngle)
                {
                    manager.currentTarget = characterStats;
                }
            }
        }
        #endregion

        #region switch state
        if (manager.currentTarget != null)
        {
            return pursueTargetState;
        }
        else return this;
        #endregion
    }
}

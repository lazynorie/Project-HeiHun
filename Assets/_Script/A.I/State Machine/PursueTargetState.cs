using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class PursueTargetState : State
{
    private CombatStanceState combatStanceState;
    //private EnemyManager enemyManager;
    private void Start()
    {
        AssignStateMachineManager();
        //enemyManager = GetComponentInParent<EnemyManager>();
        combatStanceState = stateMachineManager.combatStanceState;
    }
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimationHandler enemyAnimationHandler)
    {
        Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float viewableAngle = Vector3.SignedAngle(targetDirection, enemyManager.transform.forward, Vector3.up);
        HandleRotateTowardsTarget(enemyManager);//call this before navmesh location and rotation reset

        if (viewableAngle > 65 || viewableAngle <-65)
        {
            return stateMachineManager.rotateState;
        }
        if (enemyManager.isInteracting)
            return this;
        if (enemyManager.isPerformingAction)//if ai is performing action stop all movements
        {
            enemyAnimationHandler.animator.SetFloat("Vertical",0f);
            enemyAnimationHandler.animator.SetFloat("Horizontal", 0f);
            return this;
        }
        if (enemyManager.distanceFromTarget <= enemyManager.aggroRange)//if within attack range, switch to combat stance state
        {
            return combatStanceState;
        }
        //chase the target
       
        if (enemyManager.distanceFromTarget > enemyManager.aggroRange)
        {
            enemyAnimationHandler.animator.SetFloat("Vertical",1f,0.1f,Time.deltaTime);
        }
        //ResetNavmeshLocationRotation(enemyManager);
        return this;
    }
    
    private void HandleRotateTowardsTarget(EnemyManager enemyManager)
    {
        if (enemyManager.isPerformingAction)//rotate manually
        {
            Vector3 direction = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            direction.y = 0;
            direction.Normalize();
            if (direction == Vector3.zero)
            {
                direction = enemyManager.transform.forward;
            }
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyManager.transform.rotation =
                Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotationSpeed/Time.deltaTime);
        }
        else//rotate with pathfinding
        {
            Vector3 relativeDir = transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity);
            Vector3 targetVelocity = enemyManager.enemyRb.velocity;

            enemyManager.navMeshAgent.enabled = true;
            enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
            enemyManager.enemyRb.velocity = targetVelocity;
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation,
                enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
        }
    }

    private void ResetNavmeshLocationRotation(EnemyManager enemyManager)
    {
        
        enemyManager.navMeshAgent.transform.localPosition = Vector3.zero;
        enemyManager.navMeshAgent.transform.localRotation = Quaternion.identity;
    }
    
}

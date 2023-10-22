using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStanceState : State
{
    private AttackState attackState;
    private PursueTargetState pursueTargetState;
    private void Start()
    {
        AssignStateMachineManager();
        attackState = stateMachineManager.attackState;
        pursueTargetState = stateMachineManager.pursueTargetState;
    }
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimationHandler enemyAnimationHandler)
    {
        if (enemyManager.isPerformingAction)
        {
            enemyAnimationHandler.animator.SetFloat("Vertical",0f,0.1f,Time.deltaTime);
        }
        //HandleRotateTowardsTarget(enemyManager);
        //check for attack range
        if (enemyManager.currentRecoverTime <=0 && enemyManager.distanceFromTarget <= enemyManager.attackRange)
        {
            return attackState;
        }
        if (enemyManager.distanceFromTarget > enemyManager.attackRange)//return pursue state if oor 
        {
            return pursueTargetState;
        }
        if (enemyManager.distanceFromTarget < enemyManager.attackRange)
        {
            return attackState;
        }
        else return this;        
        //circle player or walk around
        // if in attack range return attack state
        
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

}

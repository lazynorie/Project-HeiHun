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
}

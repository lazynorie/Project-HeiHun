using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : State
{
    public EnemyAttackAction[] enemyAttacks;
    public EnemyAttackAction currentAttack;
    private CombatStanceState combatStanceState;
    private void Start()
    {
        AssignStateMachineManager();
        combatStanceState = stateMachineManager.combatStanceState;
    }
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimationHandler enemyAnimationHandler)
    {
        if (enemyManager.isPerformingAction) return combatStanceState;
        if (currentAttack != null)
        {
            if (enemyManager.distanceFromTarget < currentAttack.minimumDistanceNeededToAttack)//if too close, get a new attack
            {
                return combatStanceState;
            }
            else if (enemyManager.distanceFromTarget < currentAttack.maximumDistanceNeededToAttack)
            {
                if (enemyManager.viewableAngle <= currentAttack.maximumAttackAngle 
                    && enemyManager.viewableAngle >= currentAttack.minimumAttackAngle)
                {
                    if (enemyManager.currentRecoverTime <= 0 && enemyManager.isPerformingAction == false)
                    {
                        enemyAnimationHandler.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                        enemyAnimationHandler.animator.SetFloat("Horizontal", 0,0.1f,Time.deltaTime);
                        enemyAnimationHandler.PlayTargetAnimation(currentAttack.actionAnimation, true);
                        enemyManager.isPerformingAction = true;
                        enemyManager.currentRecoverTime = currentAttack.recoveryTime;
                        currentAttack = null;
                        return combatStanceState;
                    }
                }
            }
        }
        else
        {
            GetNewAttack(enemyManager);
        }
        return combatStanceState;
    }
    
    private void GetNewAttack(EnemyManager enemyManager)
    {
        int maxScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttack = enemyAttacks[i];
            if (enemyManager.distanceFromTarget <= enemyAttack.maximumDistanceNeededToAttack 
                && enemyManager.distanceFromTarget >= enemyAttack.minimumDistanceNeededToAttack)//distance check
            {
                if (enemyManager.viewableAngle <= enemyAttack.maximumAttackAngle 
                    && enemyManager.viewableAngle >= enemyAttack.minimumAttackAngle)//angle check
                {
                    maxScore += enemyAttack.attackScore;
                }
            }
        }

        int randomValue = Random.Range(0, maxScore);
        int tempScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttack = enemyAttacks[i];
            if (enemyManager.distanceFromTarget <= enemyAttack.maximumDistanceNeededToAttack 
                && enemyManager.distanceFromTarget >= enemyAttack.minimumDistanceNeededToAttack)//distance check
            {
                if (enemyManager.viewableAngle <= enemyAttack.maximumAttackAngle 
                    && enemyManager.viewableAngle >= enemyAttack.minimumAttackAngle)//angle check
                {
                    if (currentAttack != null)
                        return;
                    tempScore += enemyAttack.attackScore;
                    if (tempScore>randomValue)
                    {
                        currentAttack = enemyAttack;
                    }
                }
            }
        }
    }
}

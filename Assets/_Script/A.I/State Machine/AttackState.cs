using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : State
{
    public EnemyAttackAction[] enemyAttacks;
    public EnemyAttackAction currentAttack;
    private CombatStanceState combatStanceState;

    private bool performComboNextAttack = false;
    public bool hasPerformedAttack = false;//don't forget to reset this in combatstance state
    private void Start()
    {
        AssignStateMachineManager();
        combatStanceState = stateMachineManager.combatStanceState;
    }
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimationHandler enemyAnimationHandler)
    {
        RotateTowardsTargetWhenAttacking(enemyManager);
        if (enemyManager.distanceFromTarget >= enemyManager.aggroRange)
        {
            return stateMachineManager.pursueTargetState;
        }

        if (performComboNextAttack && enemyManager.canDoCombo )
        {
            AttackTargetWithCombo(enemyAnimationHandler, enemyManager);
        }

        if (!hasPerformedAttack)
        {
            AttackTarget(enemyAnimationHandler, enemyManager);
            RollForComboChange(enemyManager);
        }

        if (hasPerformedAttack && performComboNextAttack)
        {
            return this;
        }

        return stateMachineManager.rotateState;
    }

    private void AttackTarget(EnemyAnimationHandler animationHandler, EnemyManager enemyManager)
    {
        animationHandler.PlayTargetAnimation(currentAttack.actionAnimation,true);
        enemyManager.currentRecoverTime = currentAttack.recoveryTime; //set new cd timer
        hasPerformedAttack = true;
    }
    private void AttackTargetWithCombo(EnemyAnimationHandler animationHandler, EnemyManager enemyManager)
    {
        performComboNextAttack = false;
        enemyManager.currentRecoverTime = currentAttack.recoveryTime; //set new cd timer
        animationHandler.PlayTargetAnimation(currentAttack.actionAnimation,true);
        currentAttack = null;
    }
    /*private void GetNewAttack(EnemyManager enemyManager)
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
    }*/
    private void RotateTowardsTargetWhenAttacking(EnemyManager enemyManager)
    {
        if (enemyManager.canRotate && enemyManager.isInteracting)//rotate manually
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
    }

    private void RollForComboChange(EnemyManager enemyManager)
    {
        float comboChance = Random.Range(0, 100);
        if (enemyManager.allowAIToPeformCombos 
            && comboChance <= enemyManager.comboLikelyHood
            && currentAttack.comboAction != null)
        {
            performComboNextAttack = true;
            currentAttack = currentAttack.comboAction;
        }
        else
        {
            performComboNextAttack = false;
            currentAttack = null;
        }
    }

}

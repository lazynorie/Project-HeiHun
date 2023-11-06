using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStanceState : State
{
    public EnemyAttackAction[] enemyAttacks;
    [Range(0,1)]
    [SerializeField] private float horizontalAIMovingSpeed;
    [Range(0,1)]
    [SerializeField] private float verticalAIMovingSpeed;

    public bool randomDistinationSet;
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
        enemyAnimationHandler.animator.SetFloat("Vertical", verticalAIMovingSpeed,0.2f,Time.deltaTime);
        enemyAnimationHandler.animator.SetFloat("Horizontal",horizontalAIMovingSpeed,0.2f,Time.deltaTime);

        attackState.hasPerformedAttack = false;
        if (enemyManager.isInteracting)
        {
            enemyAnimationHandler.animator.SetFloat("Vertical",0f);
            enemyAnimationHandler.animator.SetFloat("Horizontal",0f);
            return this;
        }
        if (enemyManager.isPerformingAction) return this;
        if (enemyManager.distanceFromTarget > enemyManager.aggroRange)//return pursue state if oor 
        {
            return pursueTargetState;
        }
        if (!randomDistinationSet)
        {
            randomDistinationSet = true;
            DecideCirclingAction();
        }
        
        HandleRotateTowardsTarget(enemyManager);

        if (enemyManager.currentRecoverTime <= 0 && 
            attackState.currentAttack != null)
        {
            randomDistinationSet = false;
            return attackState;
        }
        GetNewAttack(enemyManager);
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

    private void DecideCirclingAction()
    {
        //circle with only forward 
        //circlr with running
        WalkAroundTarget();
    }
    private void WalkAroundTarget()
    {
        //call a random number b/w 0,1 or 0 to -1 for vertical movement (go to negative if you want ai to back off)
        float x = Random.Range(-1f, 1f);
        //another random number for horizontal movement
        float y = Random.Range(-1f, 1f);
        //link this number to the float 
        if (x > 0 && x <= 1)
        {
            horizontalAIMovingSpeed = 0.5f;
        }

        else if (x <= 0 && x >= -1)
        {
            horizontalAIMovingSpeed = -0.5f;
        }

        if (y > 0 &&  y <= 1)
        {
            verticalAIMovingSpeed = 0.5f;
        }

        else if (y <= 0 && y>= -1)
        {
            verticalAIMovingSpeed = -0.5f;
        }

        //call in Tick()
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
                    if (attackState.currentAttack != null)
                        return;
                    tempScore += enemyAttack.attackScore;
                    if (tempScore>randomValue)
                    {
                        attackState.currentAttack = enemyAttack;
                    }
                }
            }
        }
    }


}

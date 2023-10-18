using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyManager : CharacterManager
{
    private EnemyLocomotionManager enemyLocomotionManager;
    private NavMeshAgent navMeshAgent;
    private EnemyAnimationHandler enemyAnimationHandler;
    private EnemyStats enemyStats;
    
    public CharacterStats currentTarget;
    public State currentState;
    
    public float currentRecoverTime = 0;
    
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
        enemyAnimationHandler = GetComponentInChildren<EnemyAnimationHandler>();
        enemyStats = GetComponent<EnemyStats>();
        currentState = GetComponentInChildren<StateMachineManager>().idleState;
    }
    private void Update()
    {
        HandleRecoveryTimer();
    }

    private void FixedUpdate()
    {
        HandleStateMachine();
    }
    private void HandleStateMachine()
    {
        if (currentState != null)
        {
            State nexState = currentState.Tick(this, enemyStats, enemyAnimationHandler);
            if (nexState != null)
            {
                SwitchToNextState(nexState);
            }
        }
        /*if (enemyLocomotionManager.currentTarget == null)
        {
            enemyLocomotionManager.HandleDetection();
        }
        else if (enemyLocomotionManager.distanceFromTarget > enemyLocomotionManager.stoppingDistance)//
        {
            enemyLocomotionManager.HandleMoveToTarget();
        }
        else if (enemyLocomotionManager.distanceFromTarget <= enemyLocomotionManager.stoppingDistance)//within attack range
        {
            //perform action
            AttackTarget();
        }*/
    }

    private void SwitchToNextState(State nexState)
    {
        currentState = nexState;
    }

    private void AttackTarget()
    {
        /*if (isPerformingAction) return;
        if (currentAttack == null)
        {
            GetNewAttack();
        }
        else
        {
            isPerformingAction = true;
            currentRecoverTime = currentAttack.recoveryTime;
            enemyAnimationHandler.PlayTargetAnimation(currentAttack.actionAnimation, true);
            currentAttack = null; //IMPORTANT! reset current attack!
        }*/
    }
    private void GetNewAttack()
    {
        /*Vector3 targetDir = enemyLocomotionManager.currentTarget.transform.position - transform.position;
        float viewableAngle = Vector3.Angle(targetDir, transform.forward);
        enemyLocomotionManager.distanceFromTarget =
            Vector3.Distance(enemyLocomotionManager.currentTarget.transform.position, transform.position);

        int maxScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttack = enemyAttacks[i];
            if (enemyLocomotionManager.distanceFromTarget <= enemyAttack.maximumAttackAngle 
                && enemyLocomotionManager.distanceFromTarget >= enemyAttack.minimumAttackAngle)//distance check
            {
                if (viewableAngle <= enemyAttack.maximumAttackAngle 
                    && viewableAngle >= enemyAttack.minimumAttackAngle)//angle check
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
            if (enemyLocomotionManager.distanceFromTarget <= enemyAttack.maximumAttackAngle 
                && enemyLocomotionManager.distanceFromTarget >= enemyAttack.minimumAttackAngle)//distance check
            {
                if (viewableAngle <= enemyAttack.maximumAttackAngle 
                    && viewableAngle >= enemyAttack.minimumAttackAngle)//angle check
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
        }*/

    }

    private void HandleRecoveryTimer()
    {
        if (currentRecoverTime > 0)
        {
            currentRecoverTime -= Time.deltaTime;
        }
        if (isPerformingAction)
        {
            if (currentRecoverTime <= 0)
            {
                isPerformingAction = false;
            }
        }
    }
}

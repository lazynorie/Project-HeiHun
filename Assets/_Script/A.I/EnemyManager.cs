using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
public class EnemyManager : CharacterManager
{
    private EnemyLocomotionManager enemyLocomotionManager;
    public NavMeshAgent navMeshAgent;
    private EnemyAnimationHandler enemyAnimationHandler;
    private EnemyStats enemyStats;
    private StateMachineManager statesMgr;
    
    
    public float distanceFromTarget;
    public float viewableAngle;
    public float rotationSpeed = 25f;
    public float attackRange = 5f;

    public Rigidbody enemyRb;
    
    public CharacterStats currentTarget;
    
    public float currentRecoverTime = 0;
    
    public bool isPerformingAction;
    public bool isInteracting;
    [Header("A.I")] 
    [SerializeField] bool turnOffAI;
    public State currentState;
    public float detectionRadius = 15f;
    public bool allowAIToPeformCombos;
    [Range(0,100)]
    public int comboLikelyHood;
    [Header("detection angles")]
    [SerializeField] public float minimumDetectionAngle = -50f;
    [SerializeField] public float maximumDetectionAngle = 50f;

    protected override void Awake()
    {
        base.Awake();
        enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
        enemyAnimationHandler = GetComponentInChildren<EnemyAnimationHandler>();
        enemyStats = GetComponent<EnemyStats>();
        enemyRb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        navMeshAgent.enabled = false;
        statesMgr = GetComponentInChildren<StateMachineManager>();
        statesMgr.Initialize();
    }

    private void Start()
    {   //TODO: write a function for setting intial state
        SwitchToNextState(GetComponentInChildren<StateMachineManager>().idleState);
        enemyRb.isKinematic = false;
    }

    protected override void Update()
    {
        base.Update();
        GetParametersFromAnimationHandler();
        UpdateDistanceAndAngleFromTarget();
        HandleRecoveryTimer();
        HandleStateMachine();
        navMeshAgent.transform.localPosition = Vector3.zero;
        navMeshAgent.transform.localRotation = Quaternion.identity;//important: to fix the navmesh agent going everywhere!
    }
    private void FixedUpdate()
    {
        
    }
    private void HandleStateMachine()
    {
        if (turnOffAI) return;
        if (currentState != null)
        {
            State nexState = currentState.Tick(this, enemyStats, enemyAnimationHandler);
            if (nexState != null)
            {
                SwitchToNextState(nexState);
            }
        }
    }
    private void SwitchToNextState(State nexState)
    {
        currentState = nexState;
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
    private void SetInitialStateToIdleState()
    {
        if (currentState == null)
        {
            currentState =  statesMgr.idleState;
        }
    }
    private void UpdateDistanceAndAngleFromTarget()
    {
        if (currentTarget != null)
        {
            distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
            Vector3 targetDir = currentTarget.transform.position - transform.position;
            viewableAngle = Vector3.Angle(targetDir,transform.forward);
        }
    }
    private void GetParametersFromAnimationHandler()
    {
        isInteracting = enemyAnimationHandler.animator.GetBool("isInteracting");
        canDoCombo = enemyAnimationHandler.animator.GetBool("canDoCombo");
    }
}

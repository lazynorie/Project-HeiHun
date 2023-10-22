using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLocomotionManager : MonoBehaviour
{
    private EnemyManager enemyManager;
    private EnemyAnimationHandler enemyAnimationHandler;
    private Rigidbody rb;
    [SerializeField] private float fallingSpeed = 45;

    [Header("Character Collision")]
    [SerializeField] private CapsuleCollider characterCollider;
    [SerializeField] private CapsuleCollider collisonCollider;
    
    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyAnimationHandler = GetComponentInChildren<EnemyAnimationHandler>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Physics.IgnoreCollision(characterCollider,collisonCollider,true);
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * fallingSpeed);
    }
}

using System;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private EnemyManager enemyManager;
    public static event Action<int> OnEnemyDeath; 
    public float respawnTimer;
    public int exp = 50;
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;
    CapsuleCollider enemyCapsuleCollider; 
    [Header("Respawn time")]
    [SerializeField]
    private float time;
    
    private Animator animator;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
    }

    private void Start()
    {
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        animator = GetComponentInChildren<Animator>();
        enemyCapsuleCollider = GetComponent<CapsuleCollider>();
        isDead = false;
        time = 0f;
    }
    private int SetMaxHealthFromHealthLevel()
    {
        //在这里设置血量
        maxHealth = healthLevel * 10;
        return maxHealth;
    }
    public void TakeDamage(int damage, string animationName = "Getting Hit")
    {
        if (isDead)
            return;
        currentHealth = currentHealth - damage;
        animator.Play(animationName);
        if (currentHealth <=0)
        {
            isDead = true;
            currentHealth = 0;
            animator.Play("dead01");
            OnEnemyDeath?.Invoke(exp);
            //死亡逻辑
            //collider.enabled = false;
        }
    }
    public void TakeDamageWithOutAnimation(int damage)
    {
        if (isDead)
            return;
        currentHealth = currentHealth - damage;
        if (currentHealth <=0)
        {
            isDead = true;
            currentHealth = 0;
            OnEnemyDeath?.Invoke(exp);
            //死亡逻辑
            //collider.enabled = false;
        }
    }
    
    private void Respawn()
    {
        animator.CrossFade("Empty",0.3f);
        enemyCapsuleCollider.enabled = true;
        currentHealth = maxHealth;
        isDead = false;
        time = 0f;
        enemyManager.currentState = GetComponentInChildren<StateMachineManager>().idleState;
    }
}

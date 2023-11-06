using System;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private EnemyManager enemyManager;
    public static event Action<int> OnEnemyDeath;
    public UI_EnenmyHealthBar enenmyHealthBar;
    public float respawnTimer;
    public int exp = 50;
    CapsuleCollider enemyCapsuleCollider; 
    private Animator animator;
    private BossManager bossManager;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        bossManager = GetComponent<BossManager>();
        animator = GetComponentInChildren<Animator>();
        enenmyHealthBar = GetComponentInChildren<UI_EnenmyHealthBar>();
        enemyCapsuleCollider = GetComponent<CapsuleCollider>();
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
    }

    private void Start()
    {
        enenmyHealthBar.SetMaxHealth(maxHealth);
        isDead = false;
    }
    private int SetMaxHealthFromHealthLevel()
    {
        //在这里设置血量
        maxHealth = healthLevel * 10;
        return maxHealth;
    }
    public void TakeDamage(int damage, string animationName = "Getting Hit")
    {
        if (isDead) return;
        currentHealth -= damage;
        enenmyHealthBar.SetCurrentHealth(currentHealth);
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
        if (isDead) return;
        currentHealth -= damage;
        enenmyHealthBar.SetCurrentHealth(currentHealth);
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
        enemyManager.currentState = GetComponentInChildren<StateMachineManager>().idleState;
    }
}

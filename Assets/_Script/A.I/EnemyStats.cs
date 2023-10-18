using System;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public static event Action<int> onEnemyDeath; 
    public float respawnTimer;
    public int exp = 50;
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;
    private bool isDead;
    CapsuleCollider collider; 
    [Header("Respawn time")]
    [SerializeField]
    private float time;
    
    private Animator animator;
    private void Start()
    {
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        animator = GetComponentInChildren<Animator>();
        collider = GetComponent<CapsuleCollider>();
        isDead = false;
        time = 0f;
    }

    private void Update()
    {
    }

    private int SetMaxHealthFromHealthLevel()
    {
        //在这里设置血量
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        animator.Play("Getting Hit");

        if (currentHealth <=0)
        {
            isDead = true;
            currentHealth = 0;
            animator.Play("dead01");
            onEnemyDeath?.Invoke(exp);
            //死亡逻辑
            //collider.enabled = false;
        }
    }

    private void Respawn()
    {
        animator.CrossFade("Empty",0.3f);
        collider.enabled = true;
        currentHealth = maxHealth;
        isDead = false;
        time = 0f;
    }
}

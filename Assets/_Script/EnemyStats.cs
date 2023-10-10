using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;
    public float respawnTimer;
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;
    private bool isDead;
    CapsuleCollider collider; 
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
        if (isDead)
        {
            time += Time.deltaTime;
            if (time >= respawnTimer)
            {
                Respawn();
            }
        }
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
            //死亡逻辑
            collider.enabled = false;
        }
    }

    private void Respawn()
    {
        animator.CrossFade("Empty",0.3f);
        //animator.Play("Empty");
        transform.position = spawnPosition;
        transform.rotation = spawnRotation;
        collider.enabled = true;
        currentHealth = maxHealth;
        isDead = false;
        time = 0f;
    }
}
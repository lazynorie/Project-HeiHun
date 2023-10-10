using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [SerializeField] private int weapondamage =25;
    private Collider dmgCollider;

    private void Awake()
    {
        dmgCollider = GetComponent<Collider>();
        dmgCollider.gameObject.SetActive(true);
        dmgCollider.isTrigger = true;
        dmgCollider.enabled = false;
    }

    public void EnableDamageCollider()
    {
        dmgCollider.enabled = true;
    }

    public void DisableDamageCollider()
    {
        dmgCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(weapondamage);
            }
        }

        if (other.tag == "Enemy")
        {
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(weapondamage);
            }
        }
    }
}
using UnityEngine;
using UnityEngine.Serialization;

public class DamageCollider : MonoBehaviour
{
    public CharacterManager weaponOwner;
    public int weapondamage = 25; //pass through the weaponItem SO
    private BoxCollider dmgCollider;

    private void Awake()
    {
        dmgCollider = GetComponentInChildren<BoxCollider>();
        dmgCollider.gameObject.SetActive(true);
        dmgCollider.isTrigger = true;
        dmgCollider.enabled = false;

        //characterManager = GetComponentInParent<CharacterManager>();
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
            CharacterManager enemyManager = other.GetComponent<CharacterManager>();
            if (enemyManager!=null)
            {
                if (enemyManager.isParrying)
                {
                    //check here if you are parryable
                    weaponOwner.GetComponentInChildren<AnimationHandler>().PlayTargetAnimation("Parry_Parried", true);
                    return;
                }
            }
            if (playerStats != null)
            {
                playerStats.TakeDamage(weapondamage);
            }
        }
        else if (other.tag == "Enemy")
        {
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            CharacterManager manager = other.GetComponent<CharacterManager>();
            if (manager!=null)
            {
                if (manager.isParrying)
                {
                    //check here if you are parryable
                    weaponOwner.GetComponentInChildren<AnimationHandler>().PlayTargetAnimation("Parry_Parried", true);
                    return;
                }
            }
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(weapondamage);
            }
        }
        else if (other.tag is "Shield")
        {
            Debug.Log("You hit a shield");
            BlockingCollider shield = other.GetComponent<BlockingCollider>();
            float damageAfterBlock = weapondamage - (weapondamage * shield.blockingEffiency) / 100;
            shield.shieldOwner.GetComponent<PlayerStats>().TakeDamage(Mathf.FloorToInt(damageAfterBlock),"Block Hit");
            
        }
    }
}

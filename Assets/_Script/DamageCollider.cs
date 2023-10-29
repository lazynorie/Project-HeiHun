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
            BlockingCollider shield = other.transform.GetComponentInChildren<BlockingCollider>();
            if (enemyManager != null)
            {
                if (enemyManager.isParrying)
                {
                    //check here if you are parryable
                    weaponOwner.GetComponentInChildren<AnimationHandler>().PlayTargetAnimation("Parry_Parried", true);
                    return;
                }
                else if (enemyManager.isBlocking && shield != null)
                {
                    float damageAfterBlock = weapondamage - (weapondamage * shield.blockingEfficiency) / 100;
                    if (playerStats != null)
                    {
                        playerStats.TakeDamage((int)damageAfterBlock, "Block Impact");
                        return;
                    }
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
            BlockingCollider shield1 = other.transform.GetComponentInChildren<BlockingCollider>();

            if (manager != null)
            {
                if (manager.isParrying)
                {
                    //check here if you are parryable
                    weaponOwner.GetComponentInChildren<AnimationHandler>().PlayTargetAnimation("Parry_Parried", true);
                    return;
                }
                else if (manager.isBlocking && shield1 != null)
                {
                    float damageAfterBlock = weapondamage - (weapondamage * shield1.blockingEfficiency) / 100;
                    if (enemyStats != null)
                    {
                        enemyStats.TakeDamage((int)damageAfterBlock, "Block Impact");
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
                float damageAfterBlock = weapondamage - (weapondamage * shield.blockingEfficiency) / 100;
                shield.shieldOwner.GetComponent<PlayerStats>()
                    .TakeDamage(Mathf.FloorToInt(damageAfterBlock), "Block Hit");

            }
        }
    }
}

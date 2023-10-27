using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public CharacterManager characterManager;
    public int weapondamage = 25;
    private BoxCollider dmgCollider;

    private void Awake()
    {
        dmgCollider = GetComponent<BoxCollider>();
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
                    characterManager.GetComponentInChildren<AnimationHandler>().PlayTargetAnimation("Parried", true);
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
                    characterManager.GetComponentInChildren<AnimationHandler>().PlayTargetAnimation("Parried", true);
                    return;
                }
            }
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(weapondamage);
            }
        }
    }
}

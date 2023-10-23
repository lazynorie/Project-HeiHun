using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Health stats")]
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;
    [Header("Stamina stats")]
    [SerializeField] protected float staminaLevel;
    [SerializeField] protected float currentStamina;
    [SerializeField] protected float maxStamina;
    [SerializeField] protected float staminaRegenRate;
    public int MaxiumHealth
    {
        get { return maxHealth;}
        set { maxHealth = 10 * healthLevel; }
    }

    [Header("Death flag")] [SerializeField]
    protected bool isDead;
}

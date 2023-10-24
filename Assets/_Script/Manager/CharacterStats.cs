using UnityEngine;

public class CharacterStats : MonoBehaviour
{ 
    [Header("Health stats")]
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;
    [Header("Stamina stats")]
    [SerializeField] protected int staminaLevel;
    [SerializeField] protected float currentStamina;
    [SerializeField] protected float maxStamina;
    [SerializeField] protected float staminaRegenRate;

    [Header("Mana stats")] 
    [SerializeField] protected int manaLevel;
    [SerializeField] protected float currentMana;
    [SerializeField] protected int maxMana;
    
    [Header("Death flag")] [SerializeField]
    protected bool isDead;
}

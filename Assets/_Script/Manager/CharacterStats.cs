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
    public int manaLevel;
    public int currentMana;
    public int maxMana;
    
    [Header("Death flag")] [SerializeField]
    public bool isDead;

    [Header("Critical hit")] public int pendingCriticalDamage;
    [TextArea] public string toolstip;
}

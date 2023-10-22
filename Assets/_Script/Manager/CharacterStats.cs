using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Health stats")]
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;
    private int staminaLevel;
    public int Stamina 
    {
        get
        {
            return staminaLevel * 10 + 50;
        }
    }
    
    public int MaxiumHealth { get; set; }

    [Header("Death flag")] [SerializeField]
    protected bool isDead;
}

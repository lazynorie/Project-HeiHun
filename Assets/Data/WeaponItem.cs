using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Item/Weapon Item")]
public class WeaponItem : Item
{
    public GameObject modelPrefab;
    public bool isUnarmed;

    [Header("Damage")] public int baseDamage =25;
    public int criticalDamageMuiliplier = 4;

    [Header("Damange Absorption")] 
    [Range(0,100)]
    public float blockingEfficiency;
    
    [Header("one hand attack animation")] 
    public AttackAction ohLightAttackActionAttack1;
    public AttackAction ohLightAttackActionAttack2;
    public AttackAction ohHeavyAttackActionAttack1;
    [Header("two hand attack animation")] 
    public AttackAction thLight01;
    public AttackAction thLight02;
    public AttackAction thHeavy01;
    public AttackAction thHeavy02;
    
    [Header("Weapon Art")] 
    public AttackAction weaponArt;

    [Header("idle animation")] 
    public string right_hand_idle;
    public string left_hand_idle;
    public string th_idle;

    [Header("Stamina Cost")] 
    public int baseStamina;
    public float lightAttackMultiplier;
    public float heavyAttackMultiplier;

    [Header("Weapon Type")]
    public WeaponType weaponType;
    
    [TextArea]
    [Header("toolstip")] public String toolstip;
}

[Serializable]
public enum WeaponType
{
    MeleeWeapon,
    RangeWeapon,
    SpellCaster,
    PyroCaster,
    FaithCaster,
    Shield
}

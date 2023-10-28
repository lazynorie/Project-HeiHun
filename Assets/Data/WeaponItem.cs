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
    [FormerlySerializedAs("ohLightActionAttack1")] [Header("one hand attack animation")] 
    public AttackAction ohLightAttackActionAttack1;
    [FormerlySerializedAs("ohLightActionAttack2")] public AttackAction ohLightAttackActionAttack2;
    [FormerlySerializedAs("ohHeavyActionAttack1")] public AttackAction ohHeavyAttackActionAttack1;


    [FormerlySerializedAs("TH_light_attack_01")] [Header("two hand attack animation")] 
    public AttackAction thLight01;
    [FormerlySerializedAs("TH_light_attack_02")] public AttackAction thLight02;
    [FormerlySerializedAs("TH_heavy_attack_01")] public AttackAction thHeavy01;
    [FormerlySerializedAs("TH_heavy_attack_02")] public AttackAction thHeavy02;
    
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

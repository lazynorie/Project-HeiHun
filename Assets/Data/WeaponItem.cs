using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon Item")]
public class WeaponItem : Item
{
    public GameObject modelPrefab;
    public bool isUnarmed;

    [Header("Damage")] public int baseDamage =25;
    public int criticalDamageMuiliplier = 4;
    [Header("one hand attack animation")] 
    public ActionAttack ohLightActionAttack1;
    public ActionAttack ohLightActionAttack2;
    public ActionAttack ohHeavyActionAttack1;


    [Header("two hand attack animation")] 
    public ActionAttack TH_light_attack_01;
    public ActionAttack TH_light_attack_02;
    public ActionAttack TH_heavy_attack_01;
    public ActionAttack TH_heavy_attack_02;

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
}

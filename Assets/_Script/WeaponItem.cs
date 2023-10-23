using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon Item")]
public class WeaponItem : Item
{
    public GameObject modelPrefab;
    public bool isUnarmed;

    [Header("one hand attack animation")] 
    /*public string OH_light_attack_01;
    public string OH_light_attack_02;
    public string OH_heavy_attack;*/
    public Attack OH_light_attack_1;
    public Attack OH_light_attack_2;
    public Attack OH_heavy_attack_1;


    [Header("two hand attack animation")] 
    public string TH_light_attack_01;
    public string TH_light_attack_02;
    public string TH_heavy_attack;
    public Attack[] TH_hand_attacks;


    [Header("idle animation")] 
    public string right_hand_idle;
    public string left_hand_idle;
    public string th_idle;

    [TextAreaAttribute]
    [Header("toolstip")] public String toolstip;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/Spell")]
public class SpellItem : Item
{
    public enum SpellType
    {
        Faith,
        Magic,
        PyroSpell
    };

    public GameObject spellWarmupFX;
    public GameObject spellCastFX;
    public string spellAnimation;
    [Header("Spell Type")] public SpellType spellType;

    [Header("Spell Description")] [TextArea]
    public string spellDescription;

    public virtual void AttempToCastSpell()
    {
        Debug.Log("You attempt to cast a spell!");
    }

    public virtual void SuccessfulCastSpell()
    {
        Debug.Log("Successfully cast a spell!");
    }


}


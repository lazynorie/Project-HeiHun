using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpellItem : Item
{
    [Header("VFX and animation")]
    public GameObject spellWarmupFX;
    public GameObject spellCastFX;
    public string spellAnimation;
    [Header("Spell Type")] public SpellType spellType;

    [Header("Spell Description")] [TextArea]
    public string spellDescription;

    public virtual void AttempToCastSpell(AnimationHandler anim, PlayerStats playerStats)
    {
        Debug.Log("You attempt to cast a spell!");
    }

    public virtual void SuccessfulCastSpell(AnimationHandler anim, PlayerStats playerStats)
    {
        Debug.Log("Successfully cast a spell!");
    }


}

[System.Serializable]
public enum SpellType
{
    Faith,
    Magic,
    Pyro
};

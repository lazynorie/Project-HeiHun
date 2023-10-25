using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpellItem : Item
{
    public static event Action<int> OnSpellSuccessfullyCast; //use for mana drain
    public static event Action<int> OnAttemptToCastSpell;//use for mana check
    [Header("VFX and animation")]
    public GameObject spellWarmupFX;
    public GameObject spellCastFX;
    public string spellAnimation;
    [Header("Spell Cost")] public int manaCost;
    [Header("Spell Type")] public SpellType spellType;

    [Header("Spell Description")] [TextArea]
    public string spellDescription;

    public virtual void AttempToCastSpell(AnimationHandler anim, PlayerStats playerStats)
    {
        OnAttemptToCastSpell?.Invoke(manaCost);
        Debug.Log("You attempt to cast a spell!");
    }

    public virtual void SuccessfulCastSpell(AnimationHandler anim, PlayerStats playerStats)
    {
        OnSpellSuccessfullyCast?.Invoke(manaCost);
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


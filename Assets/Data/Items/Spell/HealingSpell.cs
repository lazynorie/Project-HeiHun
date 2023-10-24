using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Spell/Healing Spell")]
public class HealingSpell : SpellItem
{
   public int healAmount;
   public static event Action<int> OnHealingSpellCast; 
   public override void AttempToCastSpell(AnimationHandler anim, PlayerStats playerStats)
   {
      //GameObject instantiatedWarmSpellFX = Instantiate(spellWarmupFX, anim.transform);
      anim.PlayTargetAnimation(spellAnimation,true);
   }

   public override void SuccessfulCastSpell(AnimationHandler anim, PlayerStats playerStats)
   {
      //GameObject instantiateSpellFX = Instantiate(spellCastFX, anim.transform);
      OnHealingSpellCast?.Invoke(healAmount);
   }
}

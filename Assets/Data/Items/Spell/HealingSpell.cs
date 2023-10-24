using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Spell/Healing Spell")]
public class HealingSpell : SpellItem
{
   [FormerlySerializedAs("healMount")] public int healAmount;

   public override void AttempToCastSpell(AnimationHandler anim, PlayerStats playerStats)
   {
      GameObject instantiatedWarmSpellFX = Instantiate(spellWarmupFX, anim.transform);
      anim.PlayTargetAnimation(spellAnimation,true);
      base.AttempToCastSpell(anim,playerStats);
   }

   public override void SuccessfulCastSpell(AnimationHandler anim, PlayerStats playerStats)
   {
      GameObject instantiateSpellFX = Instantiate(spellCastFX, anim.transform);
      playerStats.currentHealth += healAmount;
      base.SuccessfulCastSpell(anim,playerStats);
   }
}

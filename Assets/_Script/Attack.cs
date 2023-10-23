using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Attack")]
public class Attack : ScriptableObject
{
   public string uniqueID;
   public string AttackAnimationName;
   public int attackPower;
   public int staminaConsum;
}

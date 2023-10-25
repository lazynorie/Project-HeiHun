using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Action/AttackAction")]
public class ActionAttack : ScriptableObject
{
   public string uniqueID;
   public string animationName;
   public int staminaConsum;
}

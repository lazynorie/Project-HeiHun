using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Action/Attack Action")]
public class AttackAction : ScriptableObject
{
   public string uniqueID;
   public string animationName;
   public int staminaConsum;
}

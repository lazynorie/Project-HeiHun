using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Attack")]
public class Attack : ScriptableObject
{
   public string uniqueID;
   public string animationName;
   public int attackPower;
   public int staminaConsum;
}

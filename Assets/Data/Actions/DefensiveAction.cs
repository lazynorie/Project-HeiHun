using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Action/Defensive Action")]
public class DefensiveAction : ScriptableObject
{
    public string uniqueID;
    public string animationName;
    public int staminaConsum;
}

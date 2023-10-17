using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "AI/Enemy Actions/Attack Action")]
public class EnemyAttackAction : EnemyAction
{
    //scores for AI decision, higher scores higher chances for AI to choose this action
    public int attackScore = 3;
    public float recoveryTime = 2;
    //angles required to attack
    public float maximumAttackAngle = 35;
    public float minimumAttackAngle = -35;
    //distance required to attack
    public float minimumDistanceNeededToAttack = 0;
    public float maximumDistanceNeededToAttack = 3;
}

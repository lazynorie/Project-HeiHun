using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "AI/Enemy Actions/Attack Action")]
public class EnemyAttackAction : EnemyAction
{
    [Header("Combo Related")]
    public bool isComboAction;
    public EnemyAttackAction comboAction;
    [Header("AI action score")]
    //scores for AI decision, higher scores higher chances for AI to choose this action
    public int attackScore = 3;
    public float recoveryTime = 2;
    //angles required to attack
    public float maximumAttackAngle = 35;
    public float minimumAttackAngle = -35;
    //distance required to attack
    public float minimumDistanceNeededToAttack = 0;
    public float maximumDistanceNeededToAttack = 3;

    [SerializeField]private string toolsTip;
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : State
{
    private void Start()
    {
        AssignStateMachineManager();
    }
    public override State Tick(EnemyManager manager, EnemyStats stats, EnemyAnimationHandler anim)
    {
        //select one of the attack base on an attack score system
        //if attack cannot perform(due to bad angel or too far away, select another one)
        //if attack is viable-> stop then attack
        //set recover timer(CD) to the attack's recovery time and move to CombatStanceState
        //return to combat stance
        
        return this;
    }
}

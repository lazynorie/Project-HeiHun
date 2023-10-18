using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStanceState : State
{
    private void Start()
    {
        AssignStateMachineManager();
    }
    public override State Tick(EnemyManager manager, EnemyStats stats, EnemyAnimationHandler anim)
    {
        //check for attack range
        //circle player or walk around
        // if in attack range return attack state
        // if player run out of range, return pursue state
        return this;
    }
}

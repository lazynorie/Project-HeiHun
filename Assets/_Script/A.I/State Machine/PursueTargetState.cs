using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueTargetState : State
{
    private void Start()
    {
        AssignStateMachineManager();
    }
    public override State Tick(EnemyManager manager, EnemyStats stats, EnemyAnimationHandler anim)
    {
        //chase the target
        //if within attack range, switch to combat stance state
        
        return this;
    }
}

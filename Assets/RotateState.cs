using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotateState : State
{
    public float viewbleAngle;
    void Start()
    {
        AssignStateMachineManager();    
    }

    public override State Tick(EnemyManager manager, EnemyStats stats, EnemyAnimationHandler anim)
    {
        anim.animator.SetFloat("Vertical", 0);
        anim.animator.SetFloat("Horizontal",0);
        Vector3 targetDirection = manager.currentTarget.transform.position - manager.transform.position;
        viewbleAngle = Vector3.SignedAngle(targetDirection, manager.transform.forward, Vector3.up);

        if (manager.isInteracting)
        {
            return this;
        }
        
        if (viewbleAngle >= 100 && viewbleAngle <= 180 && !manager.isInteracting)
        {
            anim.PlayTargetAnimationWithRootRotation("turn behind clockwise" ,true);
            return this;
        }
        else if (viewbleAngle < -100 && viewbleAngle > -180 && !manager.isInteracting)
        {
             anim.PlayTargetAnimationWithRootRotation("turn behind counter clockwise", true);
             return this;
        }
        else if (viewbleAngle <=-45 && viewbleAngle >= -100 && !manager.isInteracting)
        {
            anim.PlayTargetAnimationWithRootRotation("turn right", true);
            return this;
        }
        else if (viewbleAngle >=45 && viewbleAngle <=100 && !manager.isInteracting )
        {
            anim.PlayTargetAnimationWithRootRotation("turn left", true);
            return this;
        }

        return stateMachineManager.combatStanceState;
    }
}

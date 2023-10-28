using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimatorBoolInAnimation : StateMachineBehaviour
{
    public string targetBool;
    public bool status;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(targetBool, status);
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(targetBool, !status);
    }
}

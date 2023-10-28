using System;
using UnityEngine;

public class ResetAnimatorBool : StateMachineBehaviour
{
    private CharacterManager characterManager;
    public string targetBool;
    public bool status;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(targetBool, status);
    }
}

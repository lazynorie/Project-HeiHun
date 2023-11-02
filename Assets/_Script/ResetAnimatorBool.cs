using System;
using UnityEngine;

public class ResetAnimatorBool : StateMachineBehaviour
{
    private CharacterManager characterManager;

    public string isInteracting = "isInteracting";
    public bool isInteractingStatus = false;

    public string canRotate = "canRotate";
    public bool canRotateStatus = true;

    public string isRotatingWithRootMotion = "isUsingRootmotion";
    public bool isUsingRootmotion = false;

    public string isBlocking = "isBlocking";
    public bool isBlockStatus = false;

    public string isUsingRightHand = "isUsingRightHand";
    public bool isUsingRightHandStatus = false;
    
    public string isUsingLeftHand = "isUsingLeftHand";
    public bool isUsingLeftHandStatus = false;
    
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(isRotatingWithRootMotion,isUsingRootmotion);
        animator.SetBool(isBlocking,isBlockStatus);
        animator.SetBool(isInteracting,isInteractingStatus);
        animator.SetBool(canRotate,canRotateStatus);
        animator.SetBool(isUsingLeftHand,isUsingLeftHandStatus);
        animator.SetBool(isUsingRightHand,isUsingRightHandStatus);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRipsoteBool : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInParent<CharacterManager>().canBeRiposted = false;
        animator.GetComponentInParent<CharacterManager>().isParrying = false;
    }
}

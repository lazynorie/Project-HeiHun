using System;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator animator;
    protected CharacterManager characterManagerManager;

    
    public void PlayTargetAnimation(string targetAnim, bool isInteracting)
    {
        animator.applyRootMotion = isInteracting;
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnim,0.2f);
    }

    public virtual void TakeCriticalDamageAnimationEvent()
    {
        
    }
}

using System;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator animator;
    protected CharacterManager characterManagerManager;
    public bool canRotate;
    
    public void PlayTargetAnimation(string targetAnim, bool isInteracting,bool canRotate = false)
    {
        animator.applyRootMotion = isInteracting;
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnim,0.2f);
        animator.SetBool("canRotate", canRotate);
    }
    
    public void PlayTargetAnimationWithRootRotation(string targetAnim, bool isInteracting)
    {
        animator.applyRootMotion = isInteracting;
        animator.SetBool("isInteracting", isInteracting);
        animator.SetBool("isUsingRootmotion", true);
        animator.CrossFade(targetAnim,0.2f);
    }
    public virtual void TakeCriticalDamageAnimationEvent()
    {
        
    }
}

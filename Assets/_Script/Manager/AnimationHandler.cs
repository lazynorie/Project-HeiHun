using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator animator;
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

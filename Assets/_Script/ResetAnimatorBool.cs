using UnityEngine;

public class ResetAnimatorBool : StateMachineBehaviour
{
    public string targetBool1;
    public bool status1;

    public string targetBool2;
    public bool status2;

    public string targetBool3;
    public bool status3;
    
    public string targetBool4;
    public bool status4;
    
    public string targetBool5;
    public bool status5;
     //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(targetBool1, status1);
        animator.SetBool(targetBool2, status2);
        animator.SetBool(targetBool3, status3);
        animator.SetBool(targetBool4, status4);

        animator.GetComponentInParent<CharacterManager>().canBeRiposted = false;
    }
}

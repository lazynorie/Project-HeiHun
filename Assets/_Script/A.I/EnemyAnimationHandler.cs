using UnityEngine;

public class EnemyAnimationHandler : AnimationHandler
{
    private EnemyManager enemyManager;
    private void Awake()
    {
        enemyManager = GetComponentInParent<EnemyManager>();
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorMove()// velocity of enemy is totally base on animation
    {
        float delta = Time.deltaTime;
        enemyManager.enemyRb.drag = 0;
        Vector3 deltaPosition = animator.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / delta;
        enemyManager.enemyRb.velocity = velocity;
    }
    public void EnableCombo()
    {
        animator.SetBool("canDoCombo", true);
    }
    public void DisableCombo()
    {
        animator.SetBool("canDoCombo", false);
    }
}

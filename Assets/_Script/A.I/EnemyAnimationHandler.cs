using UnityEngine;

public class EnemyAnimationHandler : AnimationHandler
{
    private EnemyLocomotionManager locomotionManager;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        locomotionManager = GetComponentInParent<EnemyLocomotionManager>();
    }

    private void OnAnimatorMove()
    {
        float delta = Time.deltaTime;
        locomotionManager.enemyRb.drag = 0;
        Vector3 deltaPosition = animator.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / delta;
        locomotionManager.enemyRb.velocity = velocity;
    }
}

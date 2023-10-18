using UnityEngine;

public class EnemyAnimationHandler : AnimationHandler
{
    private EnemyManager enemyManager;
    private void Awake()
    {
        enemyManager = GetComponentInParent<EnemyManager>();
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorMove()
    {
        float delta = Time.deltaTime;
        enemyManager.enemyRb.drag = 0;
        Vector3 deltaPosition = animator.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / delta;
        enemyManager.enemyRb.velocity = velocity;
    }
}

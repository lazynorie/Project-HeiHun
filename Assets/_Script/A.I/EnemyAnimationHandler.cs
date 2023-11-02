using System;
using UnityEngine;

public class EnemyAnimationHandler : AnimationHandler
{
    private EnemyManager enemyManager;
    private EnemyStats enemyStats;
    protected void Awake()
    {
        enemyManager = GetComponentInParent<EnemyManager>();
        animator = GetComponent<Animator>();
        enemyStats = GetComponentInParent<EnemyStats>();
    }

    protected void Update()
    {
        animator.SetBool("isDead",enemyStats.isDead);
        animator.SetBool("canBeRiposted", enemyManager.canBeRiposted);
    }

    private void OnAnimatorMove()// velocity of enemy is totally base on animation
    {
        float delta = Time.deltaTime;
        enemyManager.enemyRb.drag = 0;
        Vector3 deltaPosition = animator.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / delta;
        enemyManager.enemyRb.velocity = velocity;

        if (enemyManager.isUsingRootmotion)
        {
            enemyManager.transform.rotation *= animator.deltaRotation;
        }
    }
    public void EnableCombo()
    {
        animator.SetBool("canDoCombo", true);
    }
    public void DisableCombo()
    {
        animator.SetBool("canDoCombo", false);
    }
    public override void TakeCriticalDamageAnimationEvent()
    {
        base.TakeCriticalDamageAnimationEvent();
        enemyStats.TakeDamageWithOutAnimation(enemyStats.pendingCriticalDamage);
        enemyStats.pendingCriticalDamage = 0;
    }
    public void EnableParry()
    {
        enemyManager.isParrying = true;
    }
    public void DisableParry()
    {
        enemyManager.isParrying = false;
    }
    public void EnableCanBeRiposted()
    {
        enemyManager.canBeRiposted = true;
    }
    public void DisableCanBeRiposted()
    {
        enemyManager.canBeRiposted = false;
    }
    public void CosumeStamina()
    {
        
    }
    public void CanRotate()
    {
        //animator.SetBool("canRotate",true);
    }
    public void StopRotate()
    {
        //animator.SetBool("canRotate",false);
    }
}

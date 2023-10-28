using UnityEngine;

public class PlayerAnimationHandler : AnimationHandler
{
    private PlayerManager playerManager;
    private PlayerStats playerStats;
    private Animation anim;
    public InputHandler inputHandler;
    public PlayerLocalmotion playerLocalmotion;
    private int vertical;
    private int horizontal;

    protected void Awake()
    {
        playerManager = GetComponentInParent<PlayerManager>();
        playerStats = GetComponentInParent<PlayerStats>();
        animator = GetComponent<Animator>();
        anim = GetComponent<Animation>();
        inputHandler = GetComponentInParent<InputHandler>();
        playerLocalmotion = GetComponentInParent<PlayerLocalmotion>();
        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");
        animator.SetBool("isDead", playerStats.isDead);
        animator.SetBool("canBeRiposted", playerManager.canBeRiposted);
    }

    public void Initialize()
    {
        
    }
    public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement, bool isSprinting)
    {
        #region Vertical

        float v = 0;
        if (verticalMovement>0 && verticalMovement <0.55f)
        {
            v = 0.5f;
        }
        else if (verticalMovement > 0.55f)
        {
            v = 1;
        }
        else if (verticalMovement < 0 && verticalMovement >-0.55f)
        {
            v = -0.5f;
        }
        else if (verticalMovement < -0.55f)
        {
            v = -1;
        }
        else
        {
            v = 0;
        }

        #endregion

        #region Horizontal

        float h = 0;
        if (horizontalMovement>0 && horizontalMovement <0.55f)
        {
            h = 0.5f;
        }
        else if (horizontalMovement > 0.55f)
        {
            h = 1;
        }
        else if (horizontalMovement < 0 && horizontalMovement >-0.55f)
        {
            h = -0.5f;
        }
        else if (horizontalMovement < -0.55f)
        {
            h = -1;
        }
        else
        {
            h = 0;
        }
        #endregion

        if (/*inputHandler.moveAmount>0 &&*/ isSprinting)
        {
            v = 2;
            h = horizontalMovement;
        }
        
        animator.SetFloat(vertical, v, 0.1f, Time.deltaTime);
        animator.SetFloat(horizontal,h,0.1f,Time.deltaTime);
    }
    public void CanRotate()
    {
        animator.SetBool("canRotate",true);
    }
    public void StopRotate()
    {
        animator.SetBool("canRotate",false);
    }
    public void EnableCombo()
    {
        animator.SetBool("canDoCombo", true);
    }
    public void DisableCombo()
    {
        animator.SetBool("canDoCombo", false);
    }
    public void CosumeStamina(int staminaCost)
    {   
        playerStats.DrainStamina(staminaCost);
    }

    public void EnableIFrame()
    {
        animator.SetBool("isInvulnerable", true);
    }
    public void DisableIFrame()
    {
        animator.SetBool("isInvulnerable", false);
    }
    private void OnAnimatorMove()
    {
        if (playerManager.isInteracting == false)
            return;

        float delta = Time.deltaTime;
        playerLocalmotion.rigidbody.drag = 0;
        Vector3 deltaPosition = animator.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / delta;
        playerLocalmotion.rigidbody.velocity = velocity;
    }

    public void EnableParry()
    {
        playerManager.isParrying = true;
    }
    public void DisableParry()
    {
        playerManager.isParrying = false;
    }

    public void EnableCanBeRiposted()
    {
        playerManager.canBeRiposted = true;
    }

    public void DisableCanBeRiposted()
    {
        playerManager.canBeRiposted = false;
    }
    public override void TakeCriticalDamageAnimationEvent()
    {
        base.TakeCriticalDamageAnimationEvent();
        playerStats.TakeDamageWithOutAnimation(playerStats.pendingCriticalDamage);
        playerStats.pendingCriticalDamage = 0;
    }
}

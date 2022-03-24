using UnityEngine;

public abstract class PlayerBaseState
{
    // Component references every state will need access to
    protected Rigidbody2D rb;
    protected Animator Animator;
    protected SpriteRenderer sr;

    protected float Horizontal;

    protected float MoveSpeed = 6.0f;
    protected float JumpForce = 7.0f;

    protected bool CanFlip = true;
    protected bool Grounded;

    public virtual void EnterState(PlayerStateMachine player)
    {
        rb = player.GetComponent<Rigidbody2D>();
        Animator = player.GetComponent<Animator>();
        sr = player.GetComponent<SpriteRenderer>();
    }

    public virtual void UpdateState(PlayerStateMachine player)
    {
        GroundCheck(player);

        Horizontal = Input.GetAxis("Horizontal");
        if (Horizontal > 0.0f && sr.flipX)
            Flip();
        else if (Horizontal < 0.0f && !sr.flipX)
            Flip();
    }

    public abstract void OnCollisionEnter2D(PlayerStateMachine player, Collision2D collision);
    
    protected void Flip()
    {
        if(CanFlip)
            sr.flipX = !sr.flipX;
    }

    protected void GroundCheck(PlayerStateMachine player)
    {
        Debug.DrawLine(player.transform.position, player.groundCheck.position, Color.green);
        Grounded = Physics2D.Linecast(player.transform.position, player.groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Grounded)
            Animator.SetBool("Grounded", true);
        else
            Animator.SetBool("Grounded", false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    protected Animator animator;

    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float jumpForce;
    protected float horizontal;

    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected bool grounded;

    protected bool facingRight = true;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0.0f)
            Move();

        GroundCheck();

        if (Input.GetButtonDown("Jump") && grounded)
            Jump();

        //Flips when hitting 'right' and facing left
        if (horizontal > 0 && !facingRight)
            Flip();
        //Flips when hitting 'left' and facing right
        else if (horizontal < 0 && facingRight)
            Flip();
    }

    protected virtual void Move()
    {
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }

    protected virtual void Jump()
    {
        rb.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
        animator.SetTrigger("Jump");
    }

    protected virtual void Attack()
    {
        Debug.Log("Attack selected");
    }

    protected virtual void GroundCheck()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        animator.SetBool("Grounded", grounded);
    }

    //Changes rotation of the player
    protected virtual void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

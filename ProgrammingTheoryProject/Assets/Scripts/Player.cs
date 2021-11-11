using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator animator;

    private float m_Health;
    public float health 
    {
        get { return m_Health; } 
        set 
        {
            if (value < 0.0f)
            {
                Debug.LogError("Player can't have negative health");
            }
            else
            {
                m_Health = value;
            }
        }
    }

    private float m_MaxHealth = 100.0f;
    public float maxHealth 
    { 
        get { return m_MaxHealth; }
    }

    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float jumpForce;
    protected float horizontal;
    protected float vertical;

    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected bool grounded;

    protected bool facingRight = true;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = maxHealth;
    }

    protected virtual void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0.0f)
        {
            Move();
            animator.SetBool("IsMoving", true);
        }
        else
            animator.SetBool("IsMoving", false);

        GroundCheck();

        if (Input.GetButtonDown("Attack") && grounded)
            Attack();

        if (Input.GetButtonDown("Jump") && grounded)
            Jump();

        if (vertical < 0.0f && grounded)
            Crouch();
        else
            animator.SetBool("IsCrouching", false);

        //To test the hurt animation
        if (Input.GetKeyDown(KeyCode.K))
            TakeDamage();

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
    }

    protected virtual void Crouch()
    {
        animator.SetBool("IsCrouching", true);
        rb.velocity = Vector2.zero;
    }

    protected virtual void Attack()
    {
        animator.SetTrigger("Attack");
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

    protected virtual void TakeDamage()
    {
        animator.SetTrigger("Hurt");

        if (facingRight)
            rb.AddForce(Vector2.left + new Vector2(-5.0f, 5.0f), ForceMode2D.Impulse);
        else
            rb.AddForce(Vector2.right + new Vector2(5.0f, 5.0f), ForceMode2D.Impulse);
    }

    protected virtual void Die()
    {

    }
}

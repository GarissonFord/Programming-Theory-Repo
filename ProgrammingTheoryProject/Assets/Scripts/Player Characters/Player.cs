using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    // Component references
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected Animator animator;

    // Current health of player
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

    protected Healthbar healthbar;

    // Will eventually implement enemies that deal damage as a fraction of the player's max health
    private float m_MaxHealth = 100.0f;
    public float maxHealth 
    { 
        get { return m_MaxHealth; }
    }

    public bool vulnerable { get; private set; }

    // Movement fields
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float jumpForce;
    protected float horizontal;
    protected float vertical;

    // Stores information on whether the player is touching the ground
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected bool grounded;

    // Animator states
    private AnimatorStateInfo animatorState;
    protected int currentState;
    protected int attackState;
    protected int hurtState;

    [SerializeField] protected bool facingRight = true;
    [SerializeField] protected bool canFlip;

    [SerializeField] protected bool hurt;
    //[SerializeField] protected bool vulnerable;
    [SerializeField] protected float knockbackForce;

    [SerializeField] protected GameObject attackHitBox;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        health = maxHealth;
    }

    protected virtual void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // Do not want the player manipulating their move direction during the hurt animation
        if (horizontal != 0.0f && !hurt)
        {
            Move();
            animator.SetBool("IsMoving", true);
        }
        else
            animator.SetBool("IsMoving", false);

        GroundCheck();

        // Triggers the end of the hurt animation
        if (grounded)
            hurt = false;

        if (Input.GetButtonDown("Attack") && grounded)
            Attack();

        if (Input.GetButtonDown("Jump") && grounded)
            Jump();

        if (vertical < 0.0f && grounded)
            Crouch();
        else
            animator.SetBool("IsCrouching", false);

        //Determines what animation state is currently playing
        animatorState = animator.GetCurrentAnimatorStateInfo(0);
        currentState = animatorState.fullPathHash;

        // Prevents the player from moving while attack animation is playing
        if (currentState == attackState)
        {
            rb.velocity = Vector2.zero;
            canFlip = false;
        }
        else
            canFlip = true;

        if (currentState == hurtState)
            hurt = true;
        else
        {
            hurt = false;
            vulnerable = true;
        }

        // Can not flip the player while they're hurt and being knocked back
        if (hurt)
        {
            canFlip = false;
        }

        // To test the hurt animation
        if (Input.GetKeyDown(KeyCode.K))
            TakeDamage(0);

        // Flips when hitting 'right' and facing left
        if (horizontal > 0 && !facingRight && canFlip)
            Flip();
        // Flips when hitting 'left' and facing right
        else if (horizontal < 0 && facingRight && canFlip)
            Flip();

        animator.SetFloat("Velocity", Mathf.Abs(rb.velocity.x));
        //Debug.Log(Mathf.Abs(rb.velocity.x));
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

    public virtual void TakeDamage(int damageTaken)
    {
        health -= damageTaken;
        if (health <= 0.0f)
            PlayerDeath();
        else

        animator.SetTrigger("Hurt");   
        hurt = true;
        Knockback();
        vulnerable = false;
        StartCoroutine(DamageFlash());
    }

    private void Knockback()
    {
        // Set the velocity to zero before knocking the player back
        rb.velocity = Vector2.zero;
        if (facingRight)
            rb.AddForce((Vector2.left + Vector2.up) * knockbackForce, ForceMode2D.Impulse);
        else
            rb.AddForce((Vector2.right + Vector2.up) * knockbackForce, ForceMode2D.Impulse);
    }

    private IEnumerator DamageFlash()
    {
        //Debug.Log("Started DamageFlash coroutine");
        while(vulnerable == false)
        {
            sr.color = Color.clear;
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.25f);
        //Debug.Log("Ended DamageFlash coroutine");
    }

    protected virtual void PlayerDeath()
    {

    }
}

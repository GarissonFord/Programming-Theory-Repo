using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ABSTRACTION
public abstract class Player : MonoBehaviour
{
    GameManager gameManager;

    // Component references
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected Animator animator;

    // Audio
    [SerializeField] protected AudioSource playerAudioSource;
    [SerializeField] protected AudioClip playerHurtAudio;
    [SerializeField] protected AudioClip playerDeathAudio;

    // Current health of player
    // ENCAPSULATION
    [SerializeField] 
    private int m_Health = 100;
    public int health {
        get { return m_Health; } 
        set
        {
            if (value <= 0) {
                m_Health = 0;
                PlayerDeath();
            } else {
                m_Health = value;
            }
        }
    }

    //protected Healthbar healthbar;
    [SerializeField] private Text healthText;

    // Will eventually implement enemies that deal damage as a fraction of the player's max health
    // ENCAPSULATION
    private int m_MaxHealth = 100;
    public int maxHealth { 
        get { return m_MaxHealth; }
    }

    // ENCAPSULATION
    [SerializeField] public bool vulnerable { get; set; }

    // Movement fields
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float maxSpeed;
    [SerializeField] protected float minimumInputThreshold;
    [SerializeField] protected float jumpForce;
    [SerializeField] public float horizontal;
    [SerializeField] public float vertical;
    [SerializeField] protected bool canMove;

    // Stores information on whether the player is touching the ground
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected bool grounded;

    // Animator states
    private AnimatorStateInfo animatorState;
    protected int currentState;
    protected int attackState;
    protected int hurtState;

    [SerializeField] public bool facingRight = true;
    [SerializeField] protected bool canFlip;

    [SerializeField] protected bool hurt;
    [SerializeField] protected float knockbackForce;

    [SerializeField] protected GameObject attackHitBox;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        healthText = GameObject.Find("Health Text").GetComponent<Text>();
        healthText.text = "Health: " + health;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerAudioSource = GetComponent<AudioSource>();
    }

    protected virtual void FixedUpdate()
    {
        // Do not want the player manipulating their move direction during the hurt animation
        if (horizontal != 0.0f && hurt != true)
        {
            Move();
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    protected virtual void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        GroundCheck();

        // Triggers the end of the hurt animation
        if (grounded)
        { 
            hurt = false;
            //canMove = true;
        }

        if (Input.GetButtonDown("Attack") && grounded)
        {
            Attack();
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
        }

        if (vertical < 0.0f && grounded)
        {
            Crouch();
        }
        else
        {
            animator.SetBool("IsCrouching", false);
        }

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
        {
            canFlip = true;
        }

        if (currentState == hurtState)
        {
            vulnerable = false;
            hurt = true;
        }
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
        {
            TakeDamage(0);
        }

        // Flips when hitting 'right' and facing left
        if (horizontal > 0 && !facingRight && canFlip)
        {
            Flip();
        } 
        // Flips when hitting 'left' and facing right
        else if (horizontal < 0 && facingRight && canFlip)
        {
            Flip();
        }

        animator.SetFloat("Velocity", Mathf.Abs(rb.velocity.x));
    }

    protected virtual void Move()
    {
        Debug.Log("Current x velocity: " + rb.velocity.x);
        float xVelocity = Mathf.Abs(rb.velocity.x);
        if (!hurt && xVelocity < maxSpeed)
        {
            rb.velocity += new Vector2(horizontal * moveSpeed, 0.0f);
        }
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
        hurt = true;
        health -= damageTaken;
        healthText.text = "Health: " + health;

        if (health <= 0.0f)
        {
            PlayerDeath();
        }
        else
        {
            animator.SetTrigger("Hurt");
            hurt = true;
            vulnerable = false;
            StartCoroutine(DamageFlash());

            playerAudioSource.clip = playerHurtAudio;
            playerAudioSource.Play();
        }

        Knockback();
    }
    
    private void Knockback()
    {
        rb.AddForce(-rb.velocity, ForceMode2D.Impulse);
        //Debug.Log("x velocity after initial force: " + rb.velocity.x);

        Vector2 knockbackVector;
        
        if (facingRight)
        {
            knockbackVector = (Vector2.left + Vector2.up) * knockbackForce;
        }
        else
        {
            knockbackVector = (Vector2.right + Vector2.up) * knockbackForce;
        }
        
        //Debug.Log("knockbackVector: " + knockbackVector);
        rb.AddForce(knockbackVector, ForceMode2D.Impulse);
    }

    private IEnumerator DamageFlash()
    {
        //Debug.Log("Started DamageFlash coroutine");
        while(hurt == true)
        {
            sr.color = Color.clear;
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.25f);
        //Debug.Log("Ended DamageFlash coroutine");
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Spikes"))
        {
            Debug.Log("Collided with spikes");
            TakeDamage(health);
        }

        if (collision.gameObject.CompareTag("Goal"))
        {
            gameManager.LevelComplete();
            moveSpeed = 0.0f;
            ParticleSystem particleSystem = GetComponentInChildren<ParticleSystem>();
            particleSystem.Play();
        }
    }

    protected virtual void PlayerDeath()
    {
        playerAudioSource.PlayOneShot(playerDeathAudio);
        
        StartCoroutine(KillOnAnimationEnd());
    }

    private IEnumerator KillOnAnimationEnd()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(1.0f);
        gameManager.GameOver();
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}

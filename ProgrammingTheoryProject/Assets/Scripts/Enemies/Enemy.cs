using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ABSTRACTION
public abstract class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator animator;
    protected SpriteRenderer sr;
    [SerializeField] protected AudioSource enemyDamageAudioSource;

    [SerializeField]
    private float m_Health;
    protected float health
    {
        // ENCAPSULATION
        get { return m_Health; }
        set
        {
            if (value <= 0.0f)
            {
                m_Health = 0.0f;
                Die();
                // Debug.LogError("Enemy can't have negative health");
            }
            else
            {
                m_Health = value;
            }
        }
    }

    // Damage the enemy can deal
    [SerializeField] public float damage;
    [SerializeField] protected float moveSpeed;

    protected bool facingRight = true;

    [SerializeField] protected AnimationClip deathAnimation;

    // Animator states
    private AnimatorStateInfo animatorState;
    protected int currentState;
    protected int deathState;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        enemyDamageAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        animatorState = animator.GetCurrentAnimatorStateInfo(0);
        currentState = animatorState.fullPathHash;

        if (currentState == deathState)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            Move();
        }
    }

    // ABSTRACTION
    protected abstract void Move();

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
        enemyDamageAudioSource.Play();
        StartCoroutine(DamageFlash());
    }

    private IEnumerator DamageFlash()
    {
        //Debug.Log("Started Enemy DamageFlash coroutine");
        sr.color = Color.clear;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        //Debug.Log("Ended Enemy DamageFlash coroutine");
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.otherCollider.attachedRigidbody;
            //Debug.Log("Collided with player");
            Player playerScript = collision.gameObject.GetComponent<Player>();
            if (playerScript.vulnerable)
            {
                /*
                if (playerScript.facingRight)
                {
                    playerRb.AddForce((Vector2.left + Vector2.up) * 5.0f, ForceMode2D.Impulse);
                }
                else
                {
                    playerRb.AddForce((Vector2.right + Vector2.up) * 5.0f, ForceMode2D.Impulse);
                }
                */
                //Debug.Log("Player is vulnerable");
                collision.gameObject.SendMessageUpwards("TakeDamage", damage);
            }
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.attachedRigidbody;
            //Debug.Log("Collided with player");
            Player playerScript = collision.gameObject.GetComponent<Player>();
            if (playerScript.vulnerable)
            {
                /*
                if (playerScript.facingRight)
                {
                    playerRb.AddForce((Vector2.left + Vector2.up) * 5.0f, ForceMode2D.Impulse);
                }
                else
                {
                    playerRb.AddForce((Vector2.right + Vector2.up) * 5.0f, ForceMode2D.Impulse);
                }
                */
                //Debug.Log("Player is vulnerable");
                collision.gameObject.SendMessageUpwards("TakeDamage", damage);
            }
        }
    }

    protected virtual void Die()
    {
        // Prevents the player from taking damage during the death animation
        GetComponent<Collider2D>().enabled = false;
        animator.SetTrigger("Death");
        StartCoroutine(DeactivateOnAnimationEnd());
    }

    protected IEnumerator DeactivateOnAnimationEnd()
    {
        // Freezes movement of the rigidbody when death animation is playing
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(deathAnimation.length);
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}

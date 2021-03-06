using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator animator;
    protected SpriteRenderer sr;

    private float m_Health;
    public float health
    {
        get { return m_Health; }
        set
        {
            if (value < 0.0f)
            {
                Debug.LogError("Enemy can't have negative health");
            }
            else
            {
                m_Health = value;
            }
        }
    }

    // Damage the enemy can deal
    [SerializeField] protected float damage;
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
            Move();
    }

    protected virtual void Move()
    {
        
    }

    protected virtual void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Collided with player");
            Player playerScript = collision.gameObject.GetComponent<Player>();
            if (playerScript.vulnerable)
            {
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

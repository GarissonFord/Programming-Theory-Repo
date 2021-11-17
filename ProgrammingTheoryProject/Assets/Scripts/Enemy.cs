using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
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

    // Damage the enemy can deal
    [SerializeField] protected float damage;
    [SerializeField] protected float moveSpeed;

    protected bool facingRight = true;

    // Animator states
    private AnimatorStateInfo animatorState;
    protected int currentState;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        animatorState = animator.GetCurrentAnimatorStateInfo(0);
        currentState = animatorState.fullPathHash;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.gameObject.SendMessageUpwards("TakeDamage", damage);
    }
}

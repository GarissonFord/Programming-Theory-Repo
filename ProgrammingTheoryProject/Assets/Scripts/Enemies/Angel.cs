using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : Enemy
{
    [SerializeField] private bool playerInSight;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private bool grounded;
    private Vector2 originalPosition;
    GameObject player;

    protected override void Awake()
    {
        base.Awake();
        originalPosition = transform.position;
    }

    protected override void Update()
    {
        base.Update();

        GroundCheck();
        // Regularly check for the player in sight
        animator.SetBool("PlayerInSight", playerInSight);
    }

    void GroundCheck()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInSight = true;
            player = collision.gameObject;
            Attack();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInSight = false;
    }

    private void Attack()
    {
        Debug.Log("Attacking");
        rb.MovePosition((player.transform.position - transform.position).normalized);
    }

    private void ReturnToOriginalPosition()
    {
        while (transform.position.y < originalPosition.y)
            rb.velocity = Vector2.up * (moveSpeed / 2);
    }
}

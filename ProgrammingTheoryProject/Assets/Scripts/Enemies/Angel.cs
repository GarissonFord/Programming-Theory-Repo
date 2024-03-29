using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Angel : Enemy
{
    [SerializeField] private bool playerInSight;
    private Vector2 originalPosition;
    GameObject player;

    // POLYMORPHISM
    protected override void Awake()
    {
        base.Awake();
        originalPosition = transform.position;
    }

    // POLYMORPHISM
    protected override void Update()
    {
        base.Update();

        // Regularly check for the player in sight
        animator.SetBool("PlayerInSight", playerInSight);
        if (playerInSight)
            Attack();
        else
            ReturnToOriginalPosition();
    }

    protected override void Move()
    {
        
    }

    // POLYMORPHISM
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInSight = true;
            player = collision.gameObject;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInSight = false;
    }
   
    private void Attack()
    {
        rb.MovePosition(Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed));      
    }

    private void ReturnToOriginalPosition()
    {
        rb.MovePosition(Vector2.MoveTowards(transform.position, originalPosition, moveSpeed));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : Enemy
{
    [SerializeField] private bool playerInSight;

    protected override void Update()
    {
        base.Update();

        // Regularly check for the player in sight
        animator.SetBool("PlayerInSight", playerInSight);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInSight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInSight = false;
    }
}

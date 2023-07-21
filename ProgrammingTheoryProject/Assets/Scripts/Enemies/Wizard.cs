using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Wizard : Enemy
{
    [SerializeField] private float circleCastRadius;
    [SerializeField] private bool playerInSight;
    [SerializeField] private Sprite fireSprite;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform fireballSpawn;
     private float timeSinceLastFireballSpawned;

    // POLYMORPHISM
    protected override void Update()
    {
        // Regularly check for the player in sight
        animator.SetBool("PlayerInSight", playerInSight);

        if (sr.sprite == fireSprite && (Time.time - timeSinceLastFireballSpawned >= 0.25f))
            ConjureFireball();
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
        }
    }

    // POLYMORPHISM
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInSight = false;
    }

    private void ConjureFireball()
    {
        Instantiate(fireballPrefab, fireballSpawn.transform.position, fireballSpawn.transform.rotation);
        timeSinceLastFireballSpawned = Time.time;
    }
}

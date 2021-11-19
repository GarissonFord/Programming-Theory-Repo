using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Enemy
{
    [SerializeField] private float circleCastRadius;
    [SerializeField] private bool playerInSight;
    [SerializeField] private Sprite fireSprite;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform fireballSpawn;
     private float timeSinceLastFireballSpawned;

    // Update is called once per frame
    protected override void Update()
    {
        // Regularly check for the player in sight
        animator.SetBool("PlayerInSight", playerInSight);

        if (sr.sprite == fireSprite && (Time.time - timeSinceLastFireballSpawned >= 0.25f))
            ConjureFireball();
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

    private void ConjureFireball()
    {
        Instantiate(fireballPrefab, fireballSpawn.transform.position, fireballSpawn.transform.rotation);
        timeSinceLastFireballSpawned = Time.time;
    }
}

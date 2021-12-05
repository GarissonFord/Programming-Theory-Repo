using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnContinuously : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] private float spawnPositionY;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private float timeOfLastSpawn;
    [SerializeField] private bool playerInZone;

    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(Time.time - timeOfLastSpawn < timeBetweenSpawns)
            {
                SpawnInRandomXPosition();
                timeOfLastSpawn = Time.time;
            }
        }
    }
    */

    private void Update()
    {
        if(playerInZone)
        {
            if (Time.time - timeOfLastSpawn > timeBetweenSpawns)
            {
                SpawnInRandomXPosition();
                timeOfLastSpawn = Time.time;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInZone = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInZone = false;
    }

    private void SpawnInRandomXPosition()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(gameObject.GetComponent<Collider2D>().bounds.min.x, gameObject.GetComponent<Collider2D>().bounds.max.x), spawnPositionY);
        Instantiate(enemyPrefab, spawnPosition, enemyPrefab.transform.rotation);
    }
}

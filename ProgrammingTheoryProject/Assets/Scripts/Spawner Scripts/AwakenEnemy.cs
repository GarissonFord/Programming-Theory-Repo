using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakenEnemy : MonoBehaviour
{
    public List<GameObject> enemiesToAwaken;
    private bool awakened;

    private void Start()
    {
        foreach (GameObject enemy in enemiesToAwaken)
            enemy.SetActive(false);

        awakened = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !awakened)
        {
            Awaken();
            awakened = true;
        }
    }

    private void Awaken()
    {
        foreach (GameObject enemy in enemiesToAwaken)
            enemy.SetActive(true);
    }
}

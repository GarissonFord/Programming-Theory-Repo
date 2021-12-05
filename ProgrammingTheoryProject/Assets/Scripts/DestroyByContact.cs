using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    //This script is for the hitbox used by the player whenever they attack

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            collision.gameObject.SendMessageUpwards("Die");
            //Destroy(collision.gameObject);
    }
}

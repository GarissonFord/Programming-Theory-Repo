using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    [SerializeField] private float playerAttackPower;
    //This script is for the hitbox used by the player whenever they attack

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            collision.gameObject.SendMessageUpwards("TakeDamage", playerAttackPower);
            //Destroy(collision.gameObject);
    }
}

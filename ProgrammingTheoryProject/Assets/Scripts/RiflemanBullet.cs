using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiflemanBullet : MonoBehaviour
{
    [SerializeField] private float bulletAttackPower;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SendMessageUpwards("TakeDamage", bulletAttackPower);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.SendMessageUpwards("TakeDamage", bulletAttackPower);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * moveSpeed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

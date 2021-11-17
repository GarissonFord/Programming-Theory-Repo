using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hellgato : Enemy
{
    // Update is called once per frame
    protected override void Update()
    {
        Move();
    }

    protected override void Move()
    {
        base.Move();
        rb.velocity = Vector2.left * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Patrol Point"))
        {
            Flip();
            moveSpeed = -moveSpeed;
        }
    }
}

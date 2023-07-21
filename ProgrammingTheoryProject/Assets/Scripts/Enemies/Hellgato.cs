using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Hellgato : Enemy
{
    // POLYMORPHISM
    protected override void Awake()
    {
        base.Awake();
        deathState = Animator.StringToHash("Base Layer.CemeteryEnemyDeath");
        damage = 15.0f;
    }

    // POLYMORPHISM
    protected override void Update()
    {
        base.Update();
    }

    // POLYMORPHISM
    protected override void Move()
    {
        rb.velocity = Vector2.left * moveSpeed;
    }

    // POLYMORPHISM
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.CompareTag("Patrol Point"))
        {
            Flip();
            moveSpeed = -moveSpeed;
        }
    }
}

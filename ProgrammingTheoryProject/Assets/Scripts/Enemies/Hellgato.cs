using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hellgato : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        deathState = Animator.StringToHash("Base Layer.CemeteryEnemyDeath");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void Move()
    {
        base.Move();
        rb.velocity = Vector2.left * moveSpeed;
    }

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

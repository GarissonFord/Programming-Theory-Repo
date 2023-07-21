using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Skeleton : Enemy
{
    int risingState;

    // POLYMORPHISM
    protected override void Awake()
    {
        base.Awake();
        risingState = Animator.StringToHash("Base Layer.SkeletonRise");
        deathState = Animator.StringToHash("Base Layer.CemeteryEnemyDeath");
        damage = 10.0f;
    }

    // POLYMORPHISM
    protected override void Update()
    {
        base.Update();
    }

    // POLYMORPHISM
    protected override void Move()
    {
        if (currentState == risingState)
            rb.velocity = Vector2.zero;
        else
            rb.velocity = Vector2.left * moveSpeed;
    }

    // POLYMORPHISM
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if(collision.gameObject.CompareTag("Patrol Point"))
        {
            Flip();
            moveSpeed = -moveSpeed;
        }
    }
}

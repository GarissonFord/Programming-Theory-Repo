using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Thing : Enemy
{
    // POLYMORPHISM
    protected override void Update()
    {
        base.Update();
        Move();
    }

    // POLYMORPHISM
    protected override void Move()
    {
        rb.velocity = Vector2.left * moveSpeed;
    }
}

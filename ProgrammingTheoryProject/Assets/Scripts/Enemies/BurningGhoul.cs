using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class BurningGhoul : Enemy
{
    protected override void Update()
    {
        Move();
    }

    // POLYMORPHISM
    protected override void Move()
    {
        rb.velocity = Vector2.left * moveSpeed;
    }
}

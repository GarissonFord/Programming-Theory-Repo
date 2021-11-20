using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Move();
    }

    protected override void Move()
    {
        base.Move();
        rb.velocity = Vector2.left * moveSpeed;
    }
}

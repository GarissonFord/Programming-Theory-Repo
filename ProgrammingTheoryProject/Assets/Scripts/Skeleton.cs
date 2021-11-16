using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    int risingState;

    protected override void Awake()
    {
        base.Awake();
        risingState = Animator.StringToHash("Base Layer.SkeletonRise");
    }

    protected override void Update()
    {
        base.Update();

        /*
        if (currentState == risingState)
            rb.velocity = Vector2.zero;
        else
            rb.velocity = Vector2.left * moveSpeed;
        */
    }
}

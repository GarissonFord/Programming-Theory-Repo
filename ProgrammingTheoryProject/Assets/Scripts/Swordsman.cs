using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordsman : Player
{
    protected override void Update()
    {
        base.Update();

        if (horizontal != 0.0f)
            animator.SetBool("IsMoving", true);
        else
            animator.SetBool("IsMoving", false);
    }
}

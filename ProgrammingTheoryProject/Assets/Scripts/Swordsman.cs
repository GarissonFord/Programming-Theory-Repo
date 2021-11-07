using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordsman : Player
{
    private AnimatorStateInfo animatorState;
    static int currentState;
    static int attackState = Animator.StringToHash("Base Layer.SwordsmanAttack");

    protected override void Update()
    {
        base.Update();

        //Updates the current state
        animatorState = animator.GetCurrentAnimatorStateInfo(0);
        currentState = animatorState.fullPathHash;

        if (currentState == attackState)
            rb.velocity = Vector2.zero;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monk : Player
{
    private AnimatorStateInfo animatorState;
    static int currentState;
    static int attackState = Animator.StringToHash("Base Layer.MonkStandingAttack");
    static int crouchState = Animator.StringToHash("Base Layer.MonkCrouch");

    protected override void Update()
    {
        base.Update();

        //Updates the current state
        animatorState = animator.GetCurrentAnimatorStateInfo(0);
        currentState = animatorState.fullPathHash;

        if (currentState == attackState)
            rb.velocity = Vector2.zero;

        // Crouch attack
        if (currentState == crouchState && Input.GetButtonDown("Attack"))
            animator.SetTrigger("Attack");

        // Jumping attack
        if (!grounded && Input.GetButtonDown("Attack"))
            animator.SetTrigger("Attack");
    }
}

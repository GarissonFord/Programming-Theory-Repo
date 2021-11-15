using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monk : Player
{
    int crouchAttackState = Animator.StringToHash("Base Layer.MonkCrouchAttack");
    protected override void Awake()
    {
        base.Awake();
        attackState = Animator.StringToHash("Base Layer.MonkStandingAttack");
    }

    protected override void Update()
    {
        base.Update();      
        
        // Jumping attack
        if (!grounded && Input.GetButtonDown("Attack"))
            animator.SetTrigger("Attack");
    }
}

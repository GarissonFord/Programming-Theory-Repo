using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordsman : Player
{
    protected override void Awake()
    {
        base.Awake();
        attackState = Animator.StringToHash("Base Layer.SwordsmanAttack");
        hurtState = Animator.StringToHash("Base Layer.SwordsmanHurt");
    }

    protected override void Update()
    {
        base.Update();
    }
}

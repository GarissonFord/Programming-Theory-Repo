using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifleman : Player
{
    protected override void Awake()
    {
        base.Awake();
        attackState = Animator.StringToHash("Base Layer.RiflemanAttack");
    }

    protected override void Update()
    {
        base.Update();
    }
}

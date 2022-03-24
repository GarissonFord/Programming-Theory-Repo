using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public override void EnterState(PlayerStateMachine player)
    {
        base.EnterState(player);
    }

    public override void UpdateState(PlayerStateMachine player)
    {
        base.UpdateState(player);
    }

    public override void OnCollisionEnter2D(PlayerStateMachine player, Collision2D collision)
    {

    }
}

using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateMachine player)
    {
        base.EnterState(player);

        Debug.Log("Hello from the Idle State");

        Animator.SetBool("IsMoving", false);
    }

    public override void UpdateState(PlayerStateMachine player)
    {
        base.UpdateState(player);

        if (Horizontal != 0.0f)
        {
            player.SwitchState(player.RunState);
        }

        if (Input.GetButtonDown("Jump"))
            player.SwitchState(player.JumpState);

        if (Input.GetAxisRaw("Vertical") < 0.0f)
        {
            player.SwitchState(player.CrouchState);
        }

        if (Input.GetButtonDown("Attack"))
            player.SwitchState(player.AttackState);

        if (Input.GetKeyDown(KeyCode.K))
            player.SwitchState(player.HurtState);
    }

    public override void OnCollisionEnter2D(PlayerStateMachine player, Collision2D collision)
    {

    }
}

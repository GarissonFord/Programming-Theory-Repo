using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public override void EnterState(PlayerStateMachine player)
    {
        base.EnterState(player);

        Debug.Log("Hello from the Run State");
        Animator.SetBool("IsMoving", true);
    }

    public override void UpdateState(PlayerStateMachine player)
    {
        base.UpdateState(player);

        if (Horizontal.Equals(0.0f))
            player.SwitchState(player.IdleState);
        else
            rb.velocity = new Vector2(Horizontal * MoveSpeed, rb.velocity.y);

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

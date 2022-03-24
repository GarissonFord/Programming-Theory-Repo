using UnityEngine;

public class PlayerCrouchState : PlayerBaseState
{
    public override void EnterState(PlayerStateMachine player)
    {
        base.EnterState(player);

        Debug.Log("Hello from the Crouch State");

        rb.velocity = Vector2.zero;
        Animator.SetBool("IsCrouching", true);
    }

    public override void UpdateState(PlayerStateMachine player)
    {
        base.UpdateState(player);

        if(Input.GetAxisRaw("Vertical") >= 0.0f)
        {
            Animator.SetBool("IsCrouching", false);
            player.SwitchState(player.IdleState);
        }

        if (Input.GetKeyDown(KeyCode.K))
            player.SwitchState(player.HurtState);
    }

    public override void OnCollisionEnter2D(PlayerStateMachine player, Collision2D collision)
    {

    }
}

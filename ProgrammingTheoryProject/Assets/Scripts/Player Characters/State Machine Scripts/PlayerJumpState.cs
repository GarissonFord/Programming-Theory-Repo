using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public override void EnterState(PlayerStateMachine player)
    {
        base.EnterState(player);

        Debug.Log("Hello from the Jump State");
        player.GetComponent<Animator>().SetBool("Grounded", false);
        rb.AddForce(new Vector2(0.0f, JumpForce), ForceMode2D.Impulse);
    }

    public override void UpdateState(PlayerStateMachine player)
    {
        base.UpdateState(player);

        if (Grounded)
        {
            Debug.Log("Grounded");
            Animator.SetBool("Grounded", true);

            if (Horizontal.Equals(0.0f))
                player.SwitchState(player.IdleState);
            else
                player.SwitchState(player.RunState);
        }
        else
            rb.velocity = new Vector2(Horizontal * MoveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.K))
            player.SwitchState(player.HurtState);
    }

    public override void OnCollisionEnter2D(PlayerStateMachine player, Collision2D collision)
    {
        /*
        if (collision.gameObject.CompareTag("Ground"))
            _grounded = true;
        */
    }
}

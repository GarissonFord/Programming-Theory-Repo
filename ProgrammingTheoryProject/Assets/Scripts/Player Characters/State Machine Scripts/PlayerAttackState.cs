using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private AnimatorStateInfo _currentStateInfo;

    public override void EnterState(PlayerStateMachine player)
    {
        base.EnterState(player);

        Debug.Log("Hello from the Attack State");

        rb.velocity = new Vector2(0.0f, 0.0f);
        CanFlip = false;
        Animator.SetTrigger("Attack");
    }

    public override void UpdateState(PlayerStateMachine player)
    {
        base.UpdateState(player);

        _currentStateInfo = Animator.GetCurrentAnimatorStateInfo(0);

        if(!_currentStateInfo.IsName("SwordsmanAttack"))
        {
            if(Horizontal.Equals(0.0f))
            {
                player.SwitchState(player.IdleState);
            }
            else
            {
                player.SwitchState(player.RunState);
            }
        }
    }

    public override void OnCollisionEnter2D(PlayerStateMachine player, Collision2D collision)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : PlayerBaseState
{
    private float _timeBetweenFlash = 0.1f;
    private float _timeSinceLastFlash;

    public override void EnterState(PlayerStateMachine player)
    {
        base.EnterState(player);

        Debug.Log("Hello from the Hurt State");

        CanFlip = false;
        Animator.SetTrigger("Hurt");
        Animator.SetBool("Grounded", false);      

        Knockback();

        _timeSinceLastFlash = 0.0f;
    }

    public override void UpdateState(PlayerStateMachine player)
    {
        base.UpdateState(player);

        _timeSinceLastFlash += Time.deltaTime;

        if(_timeSinceLastFlash >= _timeBetweenFlash)
        {
            DamageFlash();
        }
    }

    public override void OnCollisionEnter2D(PlayerStateMachine player, Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            sr.color = Color.white;
            Animator.SetBool("Grounded", true);
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

    private void Knockback()
    {
        rb.velocity = Vector2.zero;
        float knockbackForce = 5.0f;

        if (!sr.flipX)
            rb.AddForce((Vector2.left + Vector2.up) * knockbackForce, ForceMode2D.Impulse);
        else
            rb.AddForce((Vector2.right + Vector2.up) * knockbackForce, ForceMode2D.Impulse);
    }
    private void DamageFlash()
    {
        if (sr.color == Color.clear)
            sr.color = Color.white;
        else
            sr.color = Color.clear;

        _timeSinceLastFlash = 0.0f;
    }
}

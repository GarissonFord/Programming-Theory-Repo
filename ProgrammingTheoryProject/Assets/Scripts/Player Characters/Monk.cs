using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Monk : Player
{
    [SerializeField] private GameObject crouchAttackHitbox;
    [SerializeField] private GameObject jumpAttackHitbox;

    int crouchAttackState = Animator.StringToHash("Base Layer.MonkCrouchAttack");
    [SerializeField] private Sprite attackHitboxSprite;
    [SerializeField] private Sprite crouchAttackHitboxSprite;
    [SerializeField] private Sprite jumpAttackHitboxSprite;

    // POLYMORPHISM
    protected override void Awake()
    {
        base.Awake();
        attackState = Animator.StringToHash("Base Layer.MonkStandingAttack");
        hurtState = Animator.StringToHash("Base Layer.MonkHurt");
    }

    // POLYMORPHISM
    protected override void Update()
    {
        base.Update();      
        
        // Jumping attack
        if (!grounded && Input.GetButtonDown("Attack"))
            animator.SetTrigger("Attack");

        // Attack Hitbox
        if (sr.sprite == attackHitboxSprite)
            attackHitBox.SetActive(true);
        else
            attackHitBox.SetActive(false);

        // Crouch Attack Hitbox
        if (sr.sprite == crouchAttackHitboxSprite)
            crouchAttackHitbox.SetActive(true);
        else
            crouchAttackHitbox.SetActive(false);

        // Jump Attack Hitbox
        if (sr.sprite == jumpAttackHitboxSprite)
            jumpAttackHitbox.SetActive(true);
        else
            jumpAttackHitbox.SetActive(false);
    }
}

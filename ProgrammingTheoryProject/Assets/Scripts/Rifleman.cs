using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifleman : Player
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform standingFirePosition;
    [SerializeField] private Transform crouchingFirePosition;

    [SerializeField] private Sprite standingFireBulletSprite;
    [SerializeField] private Sprite crouchingFireBulletSprite;

    int crouchAttackState = Animator.StringToHash("Base Layer.RiflemanCrouchAttack");
    protected override void Awake()
    {
        base.Awake();
        attackState = Animator.StringToHash("Base Layer.RiflemanAttack");
    }

    protected override void Update()
    {
        base.Update();

        if (currentState == crouchAttackState)
            canFlip = false;
        else
            canFlip = true;

        if (standingFireBulletSprite == sr.sprite)
            FireBulletStanding();

        if (crouchingFireBulletSprite == sr.sprite)
            FireBulletCrouching();
    }

    protected override void Attack()
    {
        base.Attack();
        FireBulletStanding();
    }

    private void FireBullet(Transform firePosition)
    {
        Instantiate(bulletPrefab, firePosition);
    }

    private void FireBulletStanding()
    {
        FireBullet(standingFirePosition);
    }

    private void FireBulletCrouching()
    {
        FireBullet(crouchingFirePosition);
    }

}

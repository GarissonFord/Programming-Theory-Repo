using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifleman : Player
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform standingFirePosition;
    [SerializeField] private Transform crouchingFirePosition;

    [SerializeField] private Sprite standingFireBulletSprite;
    [SerializeField] private Sprite crouchingFireBulletSprite;

    int crouchAttackState = Animator.StringToHash("Base Layer.RiflemanCrouchAttack");
    private float timeSinceLastShotFired;

    protected override void Awake()
    {
        base.Awake();
        attackState = Animator.StringToHash("Base Layer.RiflemanAttack");
        hurtState = Animator.StringToHash("Base Layer.RiflemanHurt");
    }

    protected override void Update()
    {
        base.Update();

        if (sr.sprite == standingFireBulletSprite)
            FireBulletStanding();

        if (sr.sprite == crouchingFireBulletSprite)
            FireBulletCrouching();
    }

    private void FireBullet(Transform firePosition)
    {
        // Without this check, multiple bullets instantiate with one press of the attack button
        if (Time.time - timeSinceLastShotFired >= 0.25f)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePosition.transform.position, firePosition.transform.rotation);
            timeSinceLastShotFired = Time.time;
            if (facingRight)
                bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
            else
                bullet.GetComponent<Rigidbody2D>().AddForce(-transform.right * bulletSpeed, ForceMode2D.Impulse);
        }
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

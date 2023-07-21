using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Ghost : Enemy
{
    private Vector3 position;
    [SerializeField] private float frequency;
    [SerializeField] private float amplitude;
    [SerializeField] private float magnitude;

    // POLYMORPHISM
    protected override void Awake()
    {
        base.Awake();
        deathState = Animator.StringToHash("Base Layer.CemeteryEnemyDeath");
        position = transform.position;
    }

    // POLYMORPHISM
    protected override void Update()
    {
        base.Update();
    }

    // POLYMORPHISM
    protected override void Move()
    {
        // Moves the transform's horizontal position
        position -= transform.right * Time.deltaTime * moveSpeed;
        // Moves transform's vertical position in a sine wave pattern
        transform.position = position + transform.up * amplitude * Mathf.Sin(Time.time * frequency) * magnitude;
    }
}

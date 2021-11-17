using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    private Vector3 position;
    [SerializeField] private float frequency;
    [SerializeField] private float amplitude;
    [SerializeField] private float magnitude;

    protected override void Awake()
    {
        base.Awake();
        position = transform.position;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Move();
    }

    protected override void Move()
    {
        // Moves the transform's horizontal position
        position -= transform.right * Time.deltaTime * moveSpeed;
        // Moves transform's vertical position in a sine wave pattern
        transform.position = position + transform.up * amplitude * Mathf.Sin(Time.time * frequency) * magnitude;
    }
}

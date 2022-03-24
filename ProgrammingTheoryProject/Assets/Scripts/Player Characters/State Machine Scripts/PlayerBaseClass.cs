using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseClass : MonoBehaviour
{
    protected PlayerStateMachine StateMachine;
    public float Health { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        StateMachine = new PlayerStateMachine(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

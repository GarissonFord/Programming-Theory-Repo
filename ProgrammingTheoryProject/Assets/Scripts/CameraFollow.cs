using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        FindPlayer();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player == null)
            FindPlayer();

        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
    }

    public void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}

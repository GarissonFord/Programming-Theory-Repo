using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayerManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerPrefabs;
    [SerializeField] private List<GameObject> playerCharacters;
    //[SerializeField] private GameObject activePlayer;
    [SerializeField] private int activePlayerIndex;

    // Start is called before the first frame update
    void Start()
    {
        activePlayerIndex = 0;
        for (int i = 0; i < playerPrefabs.Count; i++)
        {
            // Instantiate each player
            Instantiate(playerPrefabs[i]);
            // Set all inactive except one that matches the activePlayer index
            if(i == activePlayerIndex)
            {
                playerPrefabs[i].SetActive(true);
            }
            else
            {
                playerPrefabs[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If player presses Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Change activePlayer to the previous player in the List
            SwitchPlayer(activePlayerIndex--);
        }
        // If player presses E
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // Change activePlayer to the previous player in the List
            SwitchPlayer(activePlayerIndex++);
        }
    }

    void SwitchPlayer(int i)
    {
        if (i < 0)
            i = playerPrefabs.Count - 1;
        else if (i > playerPrefabs.Count)
            i = 0;

        playerPrefabs[i].SetActive(true);
    }
}

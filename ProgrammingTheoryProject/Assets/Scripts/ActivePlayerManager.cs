using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayerManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerPrefabs;
    [SerializeField] private List<GameObject> playerCharacters;
    [SerializeField] private GameObject activePlayer;
    [SerializeField] private int activePlayerIndex;

    // Start is called before the first frame update
    void Start()
    {
        activePlayerIndex = 0;
        for (int i = 0; i < playerPrefabs.Count; i++)
        {
            // Instantiate each player
            // Set all inactive except one that matches the activePlayer index
            if(i == activePlayerIndex)
            {
                activePlayer = Instantiate(playerPrefabs[i]);
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
            AdjustPlayerIndex(--activePlayerIndex);
            SwitchPlayer(activePlayerIndex);
        }
        // If player presses E
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // Change activePlayer to the previous player in the List
            AdjustPlayerIndex(++activePlayerIndex);
            SwitchPlayer(activePlayerIndex);
        }
    }

    void AdjustPlayerIndex(int index)
    {
        if (index == -1)
            activePlayerIndex = playerPrefabs.Count - 1;
        else if (index == playerPrefabs.Count)
            activePlayerIndex = 0;
    }

    void SwitchPlayer(int i)
    {
        

        GameObject newPlayer = Instantiate(playerPrefabs[i], activePlayer.transform.position, activePlayer.transform.rotation);
        Destroy(activePlayer.gameObject);
        activePlayer = newPlayer;
    }
}

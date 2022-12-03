using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public Canvas gameOverScreen;
    public Button restartButton;
    // Use this for initialization
    void Start()
    {
        gameOverScreen = gameOverScreen.GetComponent<Canvas>();
        restartButton = restartButton.GetComponent<Button>();
        gameOverScreen.enabled = false;
    }

    public void GameOver()
    {
        //Create a menu to either quit or restart the level
        //gameOverScreen.enabled = true;
        //restartButton.enabled = true;

        gameOverScreen.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        Debug.Log("GameOver entered");
    }
    public void RestartGame()
    {
        Debug.Log("Restart Clicked");
        SceneManager.LoadScene("Level1");
    }
    public void QuitGame()
    {
        Debug.Log("Quit Clicked");
    }
}

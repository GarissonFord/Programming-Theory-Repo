using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public Canvas gameOverScreen, levelCompleteScreen;
    public Button restartButton;
    public AudioListener audioListener;

    void Start()
    {
        //gameOverScreen = gameOverScreen.GetComponent<Canvas>();
        //restartButton = restartButton.GetComponent<Button>();
        audioListener = GetComponent<AudioListener>();
    }

    public void LevelComplete()
    {
        levelCompleteScreen.gameObject.SetActive(true);
    }

    public void GameOver()
    {
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

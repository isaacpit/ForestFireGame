using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class EndGameMenu : MonoBehaviour
{
    public GameObject endGameUI;
    public static bool GameIsPaused = false;
    public TextMeshProUGUI endGameText;
    bool Win = false;
    public GameObject nextLevelButton;

    public void GameOver(bool win = false)
    {
        Win = win;

        if (win)
        {
            endGameText.text = "You Won!";
            nextLevelButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            endGameText.text = "You Lost!";
            nextLevelButton.GetComponent<Button>().interactable = false;
        }
        endGameUI.SetActive(true);
        if (SceneManager.GetActiveScene().buildIndex == (SceneManager.sceneCountInBuildSettings - 1)) nextLevelButton.SetActive(false);
        if (Time.timeScale != 0) Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void NextLevel()
    {
        if(Win)
        {
            Resume();
            if (SceneManager.GetActiveScene().buildIndex < (SceneManager.sceneCountInBuildSettings - 1)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else if (SceneManager.GetActiveScene().buildIndex == (SceneManager.sceneCountInBuildSettings - 1)) SceneManager.LoadScene(0);
        }
    }

    public void Replay()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMain()
    {
        Resume();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void Resume()
    {
        Time.timeScale = 1;
        endGameUI.SetActive(false);
        GameIsPaused = false;
    }
}

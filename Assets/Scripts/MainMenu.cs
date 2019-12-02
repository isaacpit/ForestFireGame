using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public GameObject mainPanel, logo;
    public GameObject playerCanvas, player;
    public GameObject levelSelectPanel;
    public Dropdown levelSelector;
    public GameObject mainCamera, playerCamera;
    public AudioSource audio;

    void Start()
    {
        player.transform.localEulerAngles = new Vector3(0, 180f, 0);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LevelSelectMenu()
    {
        mainPanel.SetActive(false);
        levelSelectPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        mainPanel.SetActive(true);
        levelSelectPanel.SetActive(false);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelSelector.value + 1);
    }

    public void Practice()
    {
        mainPanel.SetActive(false);
        playerCanvas.SetActive(true);
        player.GetComponent<NewCharacterController>().enabled = true;
        logo.SetActive(false);
        playerCamera.SetActive(true);
        mainCamera.SetActive(false);
        audio.Pause();
    }

    public void ExitPractice()
    {
        mainPanel.SetActive(true);
        playerCanvas.SetActive(false);
        player.GetComponent<NewCharacterController>().enabled = false;
        logo.SetActive(true);
        playerCamera.SetActive(false);
        mainCamera.SetActive(true);
        audio.Play();
    }
}

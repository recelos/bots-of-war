using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    bool isPaused = false;
    public Canvas pauseMenu;
    public Button resumeButton;
    public Button restartButton;
    public Button quitButton;

    void Start()
    {
        pauseMenu.enabled = false;
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        isPaused = true;
        pauseMenu.enabled = true;
        Time.timeScale = 0f;
    }

    void ResumeGame()
    {
        isPaused = false;
        pauseMenu.enabled = false;
        Time.timeScale = 1f;
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void QuitGame()
    {
        Application.Quit();
    }
}

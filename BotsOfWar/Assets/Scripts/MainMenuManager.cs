using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Canvas mainMenu;
    [SerializeField] GameObject howToTxt;
    [SerializeField] Button playBtn;
    [SerializeField] Button howToBtn;
    [SerializeField] Button quitBtn;
    bool isHowToClicked = false;

    private void Start() 
    {
        howToTxt.gameObject.SetActive(false);
        playBtn.onClick.AddListener(StartGame);    
        howToBtn.onClick.AddListener(ShowHowTo);    
        quitBtn.onClick.AddListener(QuitGame);    
    }

    void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    void ShowHowTo()
    {
        if (isHowToClicked)
        {
            howToTxt.gameObject.SetActive(false);
            isHowToClicked = false;
        }
        else
        {
            howToTxt.gameObject.SetActive(true);
            isHowToClicked = true;
        }
    }

    void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}

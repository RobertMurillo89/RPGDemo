using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string newVariable;

    public GameObject continueButton;

    public string loadGameScene;

    void Start()
    {
        if(PlayerPrefs.HasKey("Current_Scene"))
        {
            continueButton.SetActive(true);
        }else
            continueButton.SetActive(false);
    }

    void Update()
    {
        
    }

    public void Continue() 
    {
        SceneManager.LoadScene(loadGameScene);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(newVariable);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

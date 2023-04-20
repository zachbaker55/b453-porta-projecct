using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/*
This script is for the main menu and it's buttons
*/
public class MainMenu : MonoBehaviour
{
    public TMP_Text PlayText;
    private void Start() {
        if (GameManager.Instance.LevelCheckPoint != 0) 
        {
            PlayText.fontSize = 22;
            PlayText.text = "CONTINUE";
        }
        
    }

    public void onPlayPressed()
    {
        //Quick check to make sure scene we are loading from exists in build
        if(GameManager.Instance.LevelCheckPoint > SceneManager.sceneCountInBuildSettings || GameManager.Instance.LevelCheckPoint <= 0)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(GameManager.Instance.LevelCheckPoint);
        }
    }

    //Id like to add a quit button to the title screen.
    public void onQuitPressed()
    {
        Application.Quit();
    }


}

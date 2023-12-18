using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager: MonoBehaviour
{

    //Button new game
    [SerializeField] public Button newGameButton;

    //Button resume
    [SerializeField] public Button resumeButton;

    //Button credits
    [SerializeField] public Button creditButton;

    //Button exit
    [SerializeField] public Button exitButton;

    void Start()
    {
        PlayerPrefs.SetString("music", "on");
        PlayerPrefs.SetString("sound", "on");
        newGameButton.onClick.AddListener(() => ManageButtons(0));
        resumeButton.onClick.AddListener(() => ManageButtons(1));
        creditButton.onClick.AddListener(() => ManageButtons(2));
        exitButton.onClick.AddListener(() => ManageButtons(3));
    }

    public void ManageButtons(int i){
        if (i == 0 || i == 1){
            PlayerPrefs.SetInt("Previous Scene", 0);
            SceneManager.LoadScene(2);
        }
        else if (i == 2){
            SceneManager.LoadScene(3);
        }
        else if (i == 3){
            Application.Quit();
        }
        else{
            print("Command not available");
        }
    }
}

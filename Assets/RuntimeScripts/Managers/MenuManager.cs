using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager: MonoBehaviour
{

    //Button new game
    [SerializeField] public Button bng;

    //Button resume
    [SerializeField] public Button br;

    //Button credits
    [SerializeField] public Button bc;

    //Button exit
    [SerializeField] public Button be;

    // Start is called before the first frame update
    void Start()
    {
        bng.onClick.AddListener(() => ManageButtons(0));
        br.onClick.AddListener(() => ManageButtons(1));
        bc.onClick.AddListener(() => ManageButtons(2));
        be.onClick.AddListener(() => ManageButtons(3));
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void ManageButtons(int i){
        if (i == 0){
            SceneManager.LoadScene(1);
        }
        else if (i == 1){
            //SceneManager.LoadScene(1);
        }
        else if (i == 2){
            //SceneManager.LoadScene(1);
        }
        else if (i == 3){
            Application.Quit();
        }
        else{
            print("Command not available");
        }
    }
}

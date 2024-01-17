using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager: MonoBehaviour
{

    BackgroundMusicManager bmm;

    //Button new game
    [SerializeField] public Button newGameButton;

    //Button resume
    [SerializeField] public Button resumeButton;

    //Button credits
    [SerializeField] public Button creditButton;

    //Button exit
    [SerializeField] public Button exitButton;
    
    //Warning sign before exiting game
    [SerializeField] public GameObject quitWarning;

    //Warning sign yes button
    [SerializeField] public Button yesButton;

    //Warning sign no button
    [SerializeField] public Button noButton;

    //colors are used to remove and bring back the hover of the buttons, when the player clicks exit
    public Color white;

    public Color orange;

    //Warning sign if there are no available save slots
    [SerializeField] public GameObject saveWarning;

    //Warning sign ok button
    [SerializeField] public Button okButton;

    static string json;
    dynamic jsonObj;
        
    void Start()
    {
        yesButton.onClick.AddListener(() => ManageButtons(4));
        noButton.onClick.AddListener(() => ManageButtons(5));
        quitWarning.SetActive(false);
        okButton.onClick.AddListener(() => ManageButtons(6));
        saveWarning.SetActive(false);
        newGameButton.onClick.AddListener(() => ManageButtons(0));
        resumeButton.onClick.AddListener(() => ManageButtons(1));
        creditButton.onClick.AddListener(() => ManageButtons(2));
        exitButton.onClick.AddListener(() => ManageButtons(3));
        white = newGameButton.GetComponent<Image>().color;
        orange = exitButton.GetComponent<Image>().color;
    }

    public void ManageButtons(int i){
        if(quitWarning.activeSelf != true && saveWarning.activeSelf!=true){
            if(i == 0){
                json = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "data.json"));
                jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                jsonObj["active"] = 10;
                int load = 0;
                while(load < 5){
                    if (jsonObj["loaders"][load]["node"] == 0){
                        jsonObj["active"]= load;
                        break;
                    }
                    load++;
                }
                if(jsonObj["active"] == 10){
                    saveWarning.SetActive(true);
                    white.a = 0;
                    orange.a = 0;
                    newGameButton.GetComponent<Image>().color = white;
                    resumeButton.GetComponent<Image>().color = white;
                    creditButton.GetComponent<Image>().color = white;
                    exitButton.GetComponent<Image>().color = orange;
                }
                else{
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "data.json"), output);
                    SceneManager.LoadScene(1);
                }
            }
            else if (i == 1){
                PlayerPrefs.SetInt("Previous Scene", 0);
                SceneManager.LoadScene(2);
            }
            else if (i == 2){
                SceneManager.LoadScene(3);
            }
            else if (i == 3){
                quitWarning.SetActive(true);
                white.a = 0;
                orange.a = 0;
                newGameButton.GetComponent<Image>().color = white;
                resumeButton.GetComponent<Image>().color = white;
                creditButton.GetComponent<Image>().color = white;
                exitButton.GetComponent<Image>().color = orange;
            }
            else{
                print("Command not available 1");
            }
        }
        else{
            if(i == 4){
                Application.Quit();
            }
            else if(i == 5){
                quitWarning.SetActive(false);
                white.a = 255;
                orange.a = 255;
                newGameButton.GetComponent<Image>().color = white;
                resumeButton.GetComponent<Image>().color = white;
                creditButton.GetComponent<Image>().color = white;
                exitButton.GetComponent<Image>().color = orange;
            }
            else if(i == 6){
                saveWarning.SetActive(false);
                white.a = 255;
                orange.a = 255;
                newGameButton.GetComponent<Image>().color = white;
                resumeButton.GetComponent<Image>().color = white;
                creditButton.GetComponent<Image>().color = white;
                exitButton.GetComponent<Image>().color = orange;
            }
            else{
                print("Command not available 2");
            }
        }
        
    }
}

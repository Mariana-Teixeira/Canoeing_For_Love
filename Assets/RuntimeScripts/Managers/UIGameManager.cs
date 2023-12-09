using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameManager : MonoBehaviour
{
    DialogueManager dm;

    private readonly DataFileHandler dfh = new();
    [SerializeField] Button mainMenuButton;

    [SerializeField] Button loadMenuButton;

    [SerializeField] Button saveButton;

    [SerializeField] Button exit;
    // Start is called before the first frame update
    void Start()
    {
        mainMenuButton.onClick.AddListener(() => ManageButtons(2));
        loadMenuButton.onClick.AddListener(() => ManageButtons(1));
        saveButton.onClick.AddListener(() => ManageButtons(0));
        exit.onClick.AddListener(() => ManageButtons(3));
        dm = gameObject.GetComponent<DialogueManager>();
        
    }

    public void ManageButtons(int i){
        if(i == 0){
            dm.SaveGameWithoutExit();//mudar
            

        }
        else if (i == 1){
            dm.SaveGame();
            SceneManager.LoadScene(2);
        }
        else if(i ==2){
            dm.SaveGame();
            SceneManager.LoadScene(0);
        }
        else if(i == 3){
            Application.Quit();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameManager : MonoBehaviour
{
    DialogueManager dm;

    public static UIGameManager instance {get; set;}

    private readonly DataFileHandler dfh = new();
    [SerializeField] Button mainMenuButton;

    [SerializeField] Button loadMenuButton;

    [SerializeField] Button saveButton;

    [SerializeField] Button exit;

    [SerializeField] Button nobutton;

    [SerializeField] Button yesbutton;

    [SerializeField] CanvasGroup Warning;

    [SerializeField] CanvasGroup dialogueCanvas;

    [SerializeField] GameObject savingAnim;

    Animator anim;
    public bool waitingOnUser;

    int clickedButton = 0;
    // Start is called before the first frame update

    bool save = false;
    void Awake(){
        instance = this;
    }

    void Start()
    {
        mainMenuButton.onClick.AddListener(() => ManageButtons(2));
        loadMenuButton.onClick.AddListener(() => ManageButtons(1));
        saveButton.onClick.AddListener(() => ManageButtons(0));
        exit.onClick.AddListener(() => ManageButtons(3));
        dm = gameObject.GetComponent<DialogueManager>();
        nobutton.onClick.AddListener(()=>WarningButtons(false));
        yesbutton.onClick.AddListener(()=>WarningButtons(true));
        waitingOnUser = false;
        anim = savingAnim.GetComponent<Animator>();
        savingAnim.SetActive(false);
    }

    void Update(){
        if(savingAnim.activeSelf==true && anim.GetCurrentAnimatorStateInfo(layerIndex: 0).normalizedTime>1){
            savingAnim.SetActive(false);
            if(clickedButton == 1){
                dm.SaveGame();
                SceneManager.LoadScene(2);
            }
            if(clickedButton == 2){
                dm.SaveGame();
                SceneManager.LoadScene(0);
            }
            if(clickedButton == 3){
                dm.SaveGame();
                Application.Quit();
            }
        }
    }

    public void ManageButtons(int i){
        if(i == 0){
            savingAnim.SetActive(true);
            anim.Play("saving");            
            dm.SaveGameWithoutExit();
        }
        else if (i == 1){
            //dm.SaveGame();
            PlayerPrefs.SetInt("Previous Scene", 1);
            clickedButton = 1;
            ToggleWarningCanvas(true);
            waitingOnUser = true;
            //SceneManager.LoadScene(2);
        }
        else if(i ==2){
            //dm.SaveGame();
            clickedButton = 2;
            ToggleWarningCanvas(true);
            waitingOnUser = true;
            //SceneManager.LoadScene(0);
        }
        else if(i == 3){
            clickedButton = 3;
            ToggleWarningCanvas(true);
            waitingOnUser = true;
            //Application.Quit();
        }
    }
    public void WarningButtons(bool a){
        save = a;
        ToggleWarningCanvas(false);
        waitingOnUser = false;
        if(clickedButton == 1){
                if(save == true){
                    savingAnim.SetActive(true);
                    anim.Play("saving"); 
                }
                else{
                    SceneManager.LoadScene(2);
                }
            }
            if(clickedButton == 2){
                if(save == true){
                    savingAnim.SetActive(true);
                    anim.Play("saving"); 
                }
                else{
                    SceneManager.LoadScene(0);
                }
            }
            if(clickedButton == 3){
                if(save == true){
                    savingAnim.SetActive(true);
                    anim.Play("saving"); 
                }
                else{
                    Application.Quit();
                }
            }  
    }

    void ToggleWarningCanvas(bool boolean)
    {
        StopAllCoroutines();
        Warning.gameObject.SetActive(boolean);
        dialogueCanvas.interactable = !boolean;
        dialogueCanvas.blocksRaycasts = !boolean;
    }
}

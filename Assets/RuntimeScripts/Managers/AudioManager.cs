using System.Collections;
using System.Collections.Generic;
using DialogueTree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance = null;

    SoundGameManager sgm;
    BackgroundMusicManager bmm;

    Button sound;

    GameObject crossSound;

    GameObject crossMusic;
    Button music;

    
    private void OnEnable(){
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    } 
    private void OnDisable(){
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    private void Awake()
    {
        bmm = gameObject.AddComponent<BackgroundMusicManager>();
    }

    // Used for consistency with different scenes
    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.isLoaded && !scene.name.Contains("Credits"))
        {
            if(scene.name.Contains("Dialogue")){
                sgm = GameObject.Find("GameManager").GetComponent<SoundGameManager>();
            }
            sound = GameObject.Find("sound").GetComponent<Button>();
            music = GameObject.Find("music").GetComponent<Button>();
            crossSound = GameObject.Find("sound_cross");
            crossMusic = GameObject.Find("music_cross");
            sound.onClick.AddListener(() => MuteUnmute(0));
            music.onClick.AddListener(() => MuteUnmute(1));
            if(!scene.name.Contains("Main")){
                MuteUnmute(2);
            }
        }
    }

    

    void Start(){
        bmm.StartMusic();
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(base.gameObject); 
        }
        else
        {
            Destroy(base.gameObject);
        }
    }


    
    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString("music") == "off"){
            bmm.MuteMusic();
        }
        else if (PlayerPrefs.GetString("music") == "on"){
            bmm.PlayMusic();
        }
        if(SceneManager.GetActiveScene().name.Contains("Dialogue")){
            bmm.MuteMusic();
        }
    }

    // Audio manager logic
    // updates playerPrefs to maintain consistency throughout the game and the different scenes 
    public void MuteUnmute(int i){
        if(i == 0){
            if(PlayerPrefs.GetString("sound") == "on"){
                PlayerPrefs.SetString("sound", "off");
                crossSound.SetActive(true);
                if(SceneManager.GetActiveScene().name.Contains("Dialogue")){
                    sgm.MuteSound();
                }
                
            }
            else if (PlayerPrefs.GetString("sound") == "off"){
                PlayerPrefs.SetString("sound", "on");
                crossSound.SetActive(false);
                sgm.PlaySound();
            }
        }
        if(i == 1 && !SceneManager.GetActiveScene().name.Contains("Dialogue")){
            if(PlayerPrefs.GetString("music") == "on"){
                PlayerPrefs.SetString("music", "off");
                crossMusic.SetActive(true);
                bmm.MuteMusic();
            }
            else if (PlayerPrefs.GetString("music") == "off"){
                PlayerPrefs.SetString("music", "on");
                crossMusic.SetActive(false);
                bmm.PlayMusic();
            }
        }
        if(i == 2){
            if(PlayerPrefs.GetString("sound") == "on"){
                crossSound.SetActive(false);
            }
            else if (PlayerPrefs.GetString("sound") == "off"){
                crossSound.SetActive(true);
            }
            if(PlayerPrefs.GetString("music") == "on"){
                crossMusic.SetActive(false);
            }
            else if (PlayerPrefs.GetString("music") == "off"){
                crossMusic.SetActive(true);
            }
        }
    }


}
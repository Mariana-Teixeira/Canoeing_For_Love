using System.Collections;
using System.Collections.Generic;
using DialogueTree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour, INodeSubscriber
{

    AudioSource playSound;

    AudioSource bgdMusic;

    [SerializeField] Button sound;

    [SerializeField] GameObject crossSound;

    [SerializeField] GameObject crossMusic;
    [SerializeField] Button music;

    #region Node Publisher
    NodePublisher publisher;
    private void OnEnable(){
        if(SceneManager.GetActiveScene().name.Contains("Dialogue")){
            publisher.AddObserver(this);
        }
    } 
    private void OnDisable(){
        if(SceneManager.GetActiveScene().name.Contains("Dialogue")){
            publisher.RemoveObserver(this);
        }
    } 
    #endregion

    // // Start is called before the first frame update
    private void Awake()
    {
        MuteUnmute(2);
        publisher = GetComponent<NodePublisher>();
        playSound = gameObject.AddComponent<AudioSource>();
        bgdMusic = gameObject.AddComponent<AudioSource>();
        sound.onClick.AddListener(() => MuteUnmute(0));
        music.onClick.AddListener(() => MuteUnmute(1));
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString("sound") == "off"){
            playSound.volume = 0;
        }
        if (PlayerPrefs.GetString("music") == "off"){
            bgdMusic.volume = 0;
        }
    }

    public void MuteUnmute(int i){
        
        if(i == 0){
            if(PlayerPrefs.GetString("sound") == "on"){
                print("sound off");
                PlayerPrefs.SetString("sound", "off");
                crossSound.SetActive(true);
                playSound.volume = 0;
            }
            else if (PlayerPrefs.GetString("sound") == "off"){
                PlayerPrefs.SetString("sound", "on");
                crossSound.SetActive(false);
                playSound.volume = 1;
            }
        }
        if(i == 1 && !SceneManager.GetActiveScene().name.Contains("Dialogue")){
            if(PlayerPrefs.GetString("music") == "on"){
                PlayerPrefs.SetString("music", "off");
                crossMusic.SetActive(true);
                bgdMusic.volume = 0;
            }
            else if (PlayerPrefs.GetString("music") == "off"){
                PlayerPrefs.SetString("music", "on");
                crossMusic.SetActive(false);
                bgdMusic.volume = 1;
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

    public void OnNotifyNode(DialogueRuntimeNode node)
    {   
        if (node.DialogueEvents == null) return;
        if (!node.DialogueEvents.ContainsKey(DialogueEvents.PLAY_SOUND)) return;
        if(PlayerPrefs.GetString("sound") == "off") return;
        playSound.clip = Resources.Load("audio/" + (string)node.DialogueEvents[DialogueEvents.PLAY_SOUND]) as AudioClip;
        playSound.Play();
    }

}
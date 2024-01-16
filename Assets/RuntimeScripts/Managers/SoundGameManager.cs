using System.Collections;
using System.Collections.Generic;
using DialogueTree;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundGameManager : MonoBehaviour, INodeSubscriber
{
    // Sound effects Manager, and it's logic
    public static SoundGameManager instance {set; get;}

    public AudioSource playSound;


    #region Node Publisher
    NodePublisher publisher;

    private void Awake() {
        publisher = GetComponent<NodePublisher>();
        playSound = gameObject.AddComponent<AudioSource>();
    } 

    private void OnEnable(){
        
        publisher.AddObserver(this);
        
    } 
    private void OnDisable(){
        
        publisher.RemoveObserver(this);
        
    } 
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        

    }

    public void MuteSound(){
        playSound.volume = 0;
    }

    public void PlaySound(){
        playSound.volume = 1;
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

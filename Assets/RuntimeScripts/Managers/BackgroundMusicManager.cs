using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BackgroundMusicManager: MonoBehaviour
{
    AudioSource bgdMusic;
    // Background Music Manager, and it's logic
    void Awake(){
        bgdMusic = gameObject.AddComponent<AudioSource>();
        bgdMusic.loop = true;
        bgdMusic.clip = Resources.Load("audio/MX_CanoeMainTheme") as AudioClip;
    }
    
    public void StartMusic(){
        bgdMusic.Play();
        bgdMusic.volume = 0.5f;
    }

    public void PlayMusic(){
        bgdMusic.volume = 0.5f;
    }

    public void MuteMusic(){
        bgdMusic.volume = 0;
    }


}

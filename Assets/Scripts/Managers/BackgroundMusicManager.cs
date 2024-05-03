using UnityEngine;

public class BackgroundMusicManager: MonoBehaviour
{
    public AudioSource MusicSource;

    void Awake()
    {
        MusicSource.loop = true;
        MusicSource.clip = Resources.Load("audio/MX_CanoeMainTheme") as AudioClip;
    }
    
    public void StartMusic()
    {
        MusicSource.Play();
        MusicSource.volume = 0.5f;
    }

    public void PlayMusic()
    {
        MusicSource.volume = 0.5f;
    }

    public void MuteMusic()
    {
        MusicSource.volume = 0;
    }
}

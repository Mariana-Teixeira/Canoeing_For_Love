using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    SoundGameManager soundManager;
    BackgroundMusicManager musicManager;

    public GameObject cross;

    bool isSound = true;

    private void Awake()
    {
        musicManager = gameObject.GetComponent<BackgroundMusicManager>();
        soundManager = gameObject.GetComponent<SoundGameManager>();
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name.Contains("Menu") || SceneManager.GetActiveScene().name.Contains("Credits"))
        {
            musicManager.StartMusic();
        }
    }

    public void MuteUnmute()
    {
        if (isSound)
        {
            isSound = false;
            cross.SetActive(true);
            musicManager.MuteMusic();
            soundManager.MuteSound();

        }
        else if (!isSound)
        {
            isSound = true;
            cross.SetActive(false);
            musicManager.PlayMusic();
            soundManager.PlaySound();
        }
    }
}
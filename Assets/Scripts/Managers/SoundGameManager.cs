using DialogueTree;
using UnityEngine;
using UnityEngine.UI;

public class SoundGameManager : MonoBehaviour, INodeSubscriber
{
    public static SoundGameManager Instance {set; get;}

    public AudioSource SoundSource;

    #region Node Publisher
    NodePublisher publisher;
    private void Awake() => publisher = GetComponent<NodePublisher>();
    private void OnEnable() => publisher.AddObserver(this);
    private void OnDisable() => publisher.RemoveObserver(this);
    #endregion

    void Start()
    {
        Instance = this;
    }

    public void MuteSound()
    {
        SoundSource.volume = 0;
    }

    public void PlaySound()
    {
        SoundSource.volume = 1;
    }

    public void OnNotifyNode(DialogueRuntimeNode node)
    {
        if (node.DialogueEvents == null) return;
        if (!node.DialogueEvents.ContainsKey(DialogueEvents.PLAY_SOUND)) return;

        if(PlayerPrefs.GetString("music") == "off") return;
        SoundSource.clip = Resources.Load("audio/" + (string)node.DialogueEvents[DialogueEvents.PLAY_SOUND]) as AudioClip;
        SoundSource.Play();
    }
}

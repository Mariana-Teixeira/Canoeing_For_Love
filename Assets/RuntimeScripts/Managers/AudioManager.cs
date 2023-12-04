using System.Collections;
using System.Collections.Generic;
using DialogueTree;
using UnityEngine;

public class AudioManager : MonoBehaviour, INodeSubscriber
{
    #region Node Publisher
    NodePublisher publisher;
    private void Awake() => publisher = GetComponent<NodePublisher>();
    private void OnEnable() => publisher.AddObserver(this);
    private void OnDisable() => publisher.RemoveObserver(this);
    #endregion

    public void OnNotifyNode(DialogueRuntimeNode node)
    {
        if (node.DialogueEvents == null) return;
        if (!node.DialogueEvents.ContainsKey(DialogueEvents.PLAY_SOUND)) return;

        AudioSource playSound = gameObject.AddComponent<AudioSource>();
        playSound.clip = Resources.Load("audio/" + (string)node.DialogueEvents[DialogueEvents.PLAY_SOUND]) as AudioClip;
        playSound.Play();
    }
}
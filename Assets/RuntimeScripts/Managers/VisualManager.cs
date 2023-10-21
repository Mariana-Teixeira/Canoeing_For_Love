using DialogueTree;
using UnityEngine;

public class VisualManager : MonoBehaviour, INodeSubscriber
{
    NodePublisher publisher;
    private void Awake() => publisher = GetComponent<NodePublisher>();
    private void OnEnable() => publisher.AddObserver(this);
    private void OnDisable() => publisher.RemoveObserver(this);

    public void OnNotifyNPC(NPCNode node)
    {
        Debug.Log(node.ToString());
    }

    public void OnNotifyPlayer(PlayerNode node)
    {
        Debug.Log(node.ToString());
        Debug.Log(node.Choices.Length);
    }
}

using DialogueTree;
using UnityEngine;

public class VisualManager : MonoBehaviour, INodeSubscriber
{
    NodePublisher publisher;
    private void Awake() => publisher = GetComponent<NodePublisher>();
    private void OnEnable() => publisher.AddObserver(this);
    private void OnDisable() => publisher.RemoveObserver(this);

    public void OnNotify(DialogueRuntimeNode node)
    {
        Debug.Log(node.ToString());
    }
}

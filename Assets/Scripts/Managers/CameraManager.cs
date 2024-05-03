using DialogueTree;
using UnityEngine;

public class CameraManager : MonoBehaviour, INodeSubscriber
{
    [SerializeField] GameObject background;
    [SerializeField] GameObject character;

    CameraAnimation currentState;

    #region Node Publisher
    NodePublisher publisher;
    private void Awake() => publisher = GetComponent<NodePublisher>();
    private void OnEnable() => publisher.AddObserver(this);
    private void OnDisable() => publisher.RemoveObserver(this);
    #endregion

    void Start()
    {
        ChangeState(CameraAnimation.NORMAL);
    } 

    public void ChangeState(CameraAnimation newState)
    {
        if (newState == currentState) return;

        ExitState();
        currentState = newState;
        EnterState();
    }

    public void OnNotifyNode(DialogueRuntimeNode node)
    {
        if (node.DialogueEvents == null) return;
        if (!node.DialogueEvents.ContainsKey(DialogueEvents.ANIMATE_CAMERA)){
            ChangeState(CameraAnimation.NORMAL);
        }
        else
        {
            CameraAnimation animation = (CameraAnimation)node.DialogueEvents[DialogueEvents.ANIMATE_CAMERA];
            ChangeState(animation);
        }
    }

    void EnterState()
    {
    }

    void ExitState()
    {
        switch(currentState)
        {
            case CameraAnimation.SHAKE:
                break;
        }
    }

    public void StartShake()
    {  
    }

    public void StopShake()
    {
    }
}

public enum CameraAnimation
{
    NULL, NORMAL, SHAKE, ZOOM_IN, ZOOM_OUT
}
using UnityEngine;
using Cinemachine;
using DialogueTree;

public class CameraManager : MonoBehaviour, INodeSubscriber
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] Camera gameCamera;

    private float shakeIntensity = 1.0f;

    private CinemachineBasicMultiChannelPerlin multiChannelPerlin;

    CameraAnimation currentState;

    #region Node Publisher
    NodePublisher publisher;
    private void Awake() => publisher = GetComponent<NodePublisher>();
    private void OnEnable() => publisher.AddObserver(this);
    private void OnDisable() => publisher.RemoveObserver(this);
    #endregion

    void Start() => ChangeState(CameraAnimation.NORMAL);

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
        if (!node.DialogueEvents.ContainsKey(DialogueEvents.ANIMATE_CAMERA)) return;

        CameraAnimation animation = (CameraAnimation)node.DialogueEvents[DialogueEvents.ANIMATE_CAMERA];
        ChangeState(animation);
    }

    void EnterState()
    {
        switch(currentState)
        {
            case CameraAnimation.NORMAL:
                virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, 5, 0.05f);
                virtualCamera.transform.position = new Vector3(0, 0, -10);
                break;

            case CameraAnimation.SHAKE_UP:
                StartShake();
                break;

            case CameraAnimation.ZOOM_IN:
                virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, 3, 0.05f);
                virtualCamera.transform.position = new Vector3(4.5f, 2, -10);
                break;

            case CameraAnimation.ZOOM_OUT:
                virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, 7, 0.05f);
                virtualCamera.transform.position = new Vector3(0, 0, -10);
                break;
        }
    }

    void ExitState()
    {
        switch(currentState)
        {
            case CameraAnimation.SHAKE_UP:
                StopShake();
                break;
        }
    }

    public void StartShake()
    {
        multiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        multiChannelPerlin.m_AmplitudeGain = shakeIntensity;
    }

    public void StopShake()
    {
        multiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        multiChannelPerlin.m_AmplitudeGain = 0f;
    }
}

public enum CameraAnimation
{
    NULL, NORMAL, SHAKE_UP, ZOOM_IN, ZOOM_OUT
}
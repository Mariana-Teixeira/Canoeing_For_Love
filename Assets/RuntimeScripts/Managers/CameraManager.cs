using UnityEngine;
using Cinemachine;
using DialogueTree;
using System;

public class CameraManager : MonoBehaviour, INodeSubscriber
{
    //Camera Manager with the usage of a virtual camera. Zoom in, Zoom out, Shake and Normal are the possible States for the camera.
    // There's also the possibility of panning the camera
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    [SerializeField] GameObject background;

    [SerializeField] GameObject character;

    private float shakeIntensity = 0.1f;

    private CinemachineBasicMultiChannelPerlin multiChannelPerlin;

    CameraAnimation currentState;

    private readonly float edgeSize = 100f;
    private readonly float moveAmout = 0f;

    Vector3 charPos;

    Vector3 backPos;

    #region Node Publisher
    NodePublisher publisher;
    private void Awake() => publisher = GetComponent<NodePublisher>();
    private void OnEnable() => publisher.AddObserver(this);
    private void OnDisable() => publisher.RemoveObserver(this);
    #endregion

    void Start() {
        charPos = character.transform.position;
        backPos = background.transform.position;
        StopShake();
        ChangeState(CameraAnimation.NORMAL);
    } 

    void Update(){
        
        if(Input.mousePosition.x > Screen.width - edgeSize && currentState == CameraAnimation.NORMAL){
            virtualCamera.transform.position = new Vector3(- moveAmout * Time.deltaTime + virtualCamera.transform.position.x, 0, -10);
        }
        else if(Input.mousePosition.x < edgeSize && currentState == CameraAnimation.NORMAL){
            virtualCamera.transform.position = new Vector3(moveAmout * Time.deltaTime + virtualCamera.transform.position.x, 0, -10);
        }
        if(currentState == CameraAnimation.SHAKE_UP){
            StartShake();
        }
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
        background.transform.position = (Vector3)(UnityEngine.Random.insideUnitCircle * shakeIntensity);  
        character.transform.position = (Vector3)(UnityEngine.Random.insideUnitCircle * shakeIntensity);  
        
    }

    public void StopShake()
    {
        multiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        multiChannelPerlin.m_AmplitudeGain = 0f;
        background.transform.position = backPos;
        character.transform.position = charPos;
    }
}

public enum CameraAnimation
{
    NULL, NORMAL, SHAKE_UP, ZOOM_IN, ZOOM_OUT
}
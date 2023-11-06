using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using Cinemachine;
using System.Threading;

public class CameraEventsManager : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera cvc;

    private float shakeIntensity = 1.0f;
    private float shakeTime = 0.2f;

    private float timer;
    private CinemachineBasicMultiChannelPerlin cbmcp;
    [SerializeField] Camera gameCamera;

    [SerializeField] CameraEvents cameraEvent;

    public int run = 0; 
    void Start()
    {
        StopShake();
    }
    void Update(){
        if(cameraEvent == CameraEvents.SHAKE_UP){
            StartShake();
            if(timer > 0){
                timer -= Time.deltaTime;
                if(timer<=0){
                    StopShake();
                }
            }
        }
        else if(cameraEvent == CameraEvents.ZOOM_IN){
            cvc.m_Lens.OrthographicSize = Mathf.Lerp(cvc.m_Lens.OrthographicSize, 3, 0.05f);
            cvc.transform.position = new Vector3(4.5f, 2, -10);
            StopShake();
            
        }
        else if(cameraEvent == CameraEvents.ZOOM_OUT){
            cvc.m_Lens.OrthographicSize = Mathf.Lerp(cvc.m_Lens.OrthographicSize, 7, 0.05f);
            cvc.transform.position = new Vector3(0, 0, -10);
            StopShake();
        }
        else{
            
            float edgeSize = 100f;
            float moveAmout = 1f;
            if(Input.mousePosition.x > Screen.width - edgeSize){
                cvc.transform.position = new Vector3(- moveAmout * Time.deltaTime + cvc.transform.position.x, 0, -10);
            }
            else if(Input.mousePosition.x < edgeSize){
                cvc.transform.position = new Vector3(moveAmout * Time.deltaTime + cvc.transform.position.x, 0, -10);
            }
            cvc.m_Lens.OrthographicSize = Mathf.Lerp(cvc.m_Lens.OrthographicSize, 5, 0.05f);
            // cvc.transform.position = new Vector3(0, 0, -10);
            StopShake();
        }
    }

    public void StartShake(){
        cbmcp = cvc.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cbmcp.m_AmplitudeGain = shakeIntensity;
        timer = shakeTime;
    }

    public void StopShake(){
        cbmcp = cvc.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cbmcp.m_AmplitudeGain = 0f;
        timer = 0;
    }
}

public enum CameraEvents {
    NORMAL, SHAKE_UP, ZOOM_IN, ZOOM_OUT 
}

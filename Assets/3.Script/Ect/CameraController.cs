using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera inGameVirCam;
    CinemachineBasicMultiChannelPerlin noise;
    float originGainValue;

    void Start()
    {
        noise = inGameVirCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        originGainValue = noise.m_AmplitudeGain;
        
        print(originGainValue);
    }

    public void StartShakeCam(float _size, float _time)
    {
        if(ShakeCam != null)
        {
            StopCoroutine(ShakeCam);
        }
        ShakeCam = StartCoroutine(ShakeCam_co(_size, _time));
    }
    
    Coroutine ShakeCam;

    IEnumerator ShakeCam_co(float _size, float _time)
    {
        noise.m_AmplitudeGain = _size;
        yield return new WaitForSeconds(_time);
        noise.m_AmplitudeGain = originGainValue;
    }

    public void ShiftVirtualCam()
    {
        inGameVirCam.Priority = 100;
    }

    public void DisconnectTrace()
    {
        if (ShakeCam != null)
        {
            StopCoroutine(ShakeCam);
        }
        noise.m_AmplitudeGain = 0;
        inGameVirCam.transform.SetParent(null);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera virCam;
    CinemachineBasicMultiChannelPerlin noise;
    float originGainValue;

    void Start()
    {
        // Cinemachine Virtual Camera의 Noise 프로필 가져오기
        noise = virCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        originGainValue = noise.m_AmplitudeGain;
    }

    // 캐릭터 주변에 운석이 떨어지면 화면 흔들림,
    // 운석에 맞거나 불이나 크레이터에 부딪히면 화면 흔들림

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartShakeCam(20,.2f);
        }
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

}

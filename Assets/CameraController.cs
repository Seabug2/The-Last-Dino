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
        // Cinemachine Virtual Camera�� Noise ������ ��������
        noise = virCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        originGainValue = noise.m_AmplitudeGain;
    }

    // ĳ���� �ֺ��� ��� �������� ȭ�� ��鸲,
    // ��� �°ų� ���̳� ũ�����Ϳ� �ε����� ȭ�� ��鸲

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

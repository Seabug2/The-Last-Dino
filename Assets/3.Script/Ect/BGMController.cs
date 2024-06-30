using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMController : MonoBehaviour
{
    AudioSource audioSource;

    /// <summary>
    /// 0 : lobbyClip
    /// 1 : InGameClip
    /// 2 : gameOverClip
    /// </summary>
    [SerializeField, Tooltip("indet/0 = 시작화면 BGM, 1 = 플레이 화면 BGM, 2 = 결과화면 BGM")]
    AudioClip[] bgms;

    public static BGMController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        Init();
    }

    private void Init()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = true;
        audioSource.loop = true;
        PlayBGM(0);
    }

    private void Start()
    {
        PlayBGM(0);

        GameManager.instance.StartAction += () =>
        {
            PlayBGM(1);
        };

        GameManager.instance.GameOverAction += () =>
        {
            PlayBGM(2);
        };
    }

    public void PauseBGM()
    {
        if (!audioSource.isPlaying)
            audioSource.UnPause();
    }

    /// <summary>
    /// 
    /// </summary>
    public void UnpauseBGM()
    {
        if (!audioSource.isPlaying)
            audioSource.UnPause();
    }

    public void VolumeSet(int _num)
    {
        audioSource.volume = PlayerPrefs.GetFloat($"Volume {_num}", 1);
    }
    public void VolumeSet(float _value)
    {
        audioSource.volume = _value;
    }
    void PlayBGM(int _bgmNum)
    {
        VolumeSet(_bgmNum);
        audioSource.clip = bgms[_bgmNum];
        audioSource.Play();
    }
}

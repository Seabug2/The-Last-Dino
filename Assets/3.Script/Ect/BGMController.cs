using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMController : MonoBehaviour
{
    AudioSource audioSource;
    
    [SerializeField]
    AudioClip lobbyClip, InGameClip, gameOverClip;

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

        PlayAudio(lobbyClip);
    }

    private void Start()
    {
        GameManager.instance.StartAction += () =>
        {
            PlayAudio(InGameClip);
        };
        
        GameManager.instance.GameOverAction += () =>
        {
            PlayAudio(gameOverClip);
        };
    }


    public void BGMPlayingToggle()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        else
        {
            audioSource.Play();
        }
    }

    public void VolumeSet()
    {
        audioSource.volume = PlayerPrefs.GetFloat("BGM Volume", 1);
    }

    void PlayAudio(AudioClip _clip)
    {
        audioSource.Stop();
        audioSource.clip = _clip;
        audioSource.Play();
    }
}

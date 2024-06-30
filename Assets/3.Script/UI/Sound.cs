using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    [SerializeField] AudioSource audiosource;
    [SerializeField] Slider slider;

    public void ON_sound()
    {
        audiosource.enabled = true;

        float value = slider.value;

        audiosource.volume = value;
    }

    public void OFF_sound()
    {
        audiosource.enabled = false;
    }
}
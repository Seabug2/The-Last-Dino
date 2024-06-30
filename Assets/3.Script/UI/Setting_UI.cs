using UnityEngine;
using UnityEngine.UI;

public class Setting_UI : MonoBehaviour
{
    [SerializeField]
    int sliderNum;
    Slider slider;
    AudioSource audioSource;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        audioSource = GetComponent<AudioSource>();
        slider.value = PlayerPrefs.GetFloat($"Volume {sliderNum}", 1);
    }

    /// <summary>
    /// 슬라이더를 조작하면 호출되어야 합니다.
    /// 현재 조작한 값으로 BGM의 볼륨을 조절합니다.
    /// </summary>
    public void OnChangedEvent()
    {
        float value = slider.value;
        //미리듣기 소리 조절하고
        audioSource.volume = value;
        //VolumeSave(value);
    }

    /// <summary>
    /// 이것은 대기화면의 BGM 음량을 조절하는 슬라이더에만 추가합니다.
    /// 그 슬라이더는 추가로 BGM의 볼륨을 당장 바꿔야 합니다.
    /// </summary>
    public void VolumeSet()
    {
        float value = slider.value;
        BGMController.instance.VolumeSet(value);
    }

    /// <summary>
    /// 게임시작 버튼을 클릭하면 호출 되도록 시작 버튼에 추가합니다.
    /// </summary>
    public void VolumeSave()
    {
        float value = slider.value;
        PlayerPrefs.SetFloat($"Volume {sliderNum}", value);
    }

    public void PlayMyAudio()
    {
        if (audioSource.time.Equals(0))
        {
            audioSource.Play();
        }
        else
        {
            audioSource.UnPause();
        }
    }

}
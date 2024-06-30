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
    /// �����̴��� �����ϸ� ȣ��Ǿ�� �մϴ�.
    /// ���� ������ ������ BGM�� ������ �����մϴ�.
    /// </summary>
    public void OnChangedEvent()
    {
        float value = slider.value;
        //�̸���� �Ҹ� �����ϰ�
        audioSource.volume = value;
        //VolumeSave(value);
    }

    /// <summary>
    /// �̰��� ���ȭ���� BGM ������ �����ϴ� �����̴����� �߰��մϴ�.
    /// �� �����̴��� �߰��� BGM�� ������ ���� �ٲ�� �մϴ�.
    /// </summary>
    public void VolumeSet()
    {
        float value = slider.value;
        BGMController.instance.VolumeSet(value);
    }

    /// <summary>
    /// ���ӽ��� ��ư�� Ŭ���ϸ� ȣ�� �ǵ��� ���� ��ư�� �߰��մϴ�.
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
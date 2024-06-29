using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Ÿ�̸Ӵ� ������ ���۵Ǹ� �ð��� �����ϱ� ����
    // ������ ����Ǹ� ���ӸŴ������� ������ �����ϰ� �ӹ� ����

    [SerializeField]
    Text timeText;

    private void Awake()
    {
        timeText.gameObject.SetActive(false);
    }

    void Start()
    {
        GameManager.instance.StartAction += () =>
        {
            StartCoroutine(CountTime_co());
        };
    }

    IEnumerator CountTime_co()
    {
        float score = 0;
        float startTime = Time.time;

        timeText.gameObject.SetActive(true);
        timeText.text = score.ToString();
        
        while (GameManager.instance.state.Equals(State.InGame))
        {
            yield return null;
            score = Time.time - startTime;
            timeText.text = "Time\n" + TimerConvert(score);
        }
        GameManager.instance.score = score;
        timeText.gameObject.SetActive(false);
        Destroy(this);
    }

    // �ð� ǥ�ÿ� �޼ҵ� (00:00.00)
    public static string TimerConvert(float timer)
    {
        // 123.45678...
        int min = (int)timer / 60; // 2
        string min_str = min.ToString("00"); // "02"

        int sec = (int)timer % 60; // 3
        string sec_str = sec.ToString("00"); // "3"

        int ms = (int)(timer * 100) % 100; // 45
        string ms_str = ms.ToString("00"); // "45"

        return $"{min_str}:{sec_str}<size=25>.{ms_str}</size>"; // "02:03.45"
    }
}

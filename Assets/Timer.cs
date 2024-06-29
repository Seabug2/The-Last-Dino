using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // 타이머는 게임이 시작되면 시간을 측정하기 시작
    // 게임이 종료되면 게임매니저에게 점수를 전달하고 임무 종료

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
    }

    // 시간 표시용 메소드 (00:00.00)
    string TimerConvert(float timer)
    {
        // 123.45678...
        int min = (int)timer / 60; // 2
        string min_str = min.ToString(); // "2"
        if (min < 10) min_str = "0" + min_str; // "02"

        int sec = (int)timer % 60; // 3
        string sec_str = sec.ToString(); // "3"
        if (sec < 10) sec_str = "0" + sec_str; // "03"

        int ms = (int)(timer * 100) % 100; // 45
        string ms_str = ms.ToString(); // "45"
        if (ms < 10) ms_str = "0" + ms_str; // "45"

        return $"{min_str}:{sec_str}<size=25>.{ms_str}</size>"; // "02:03.45"
    }
}

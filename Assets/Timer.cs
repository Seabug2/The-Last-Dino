using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    Text timeText;
    [SerializeField]
    public float size1 = 25f; // 분과 초의 크기 설정 변수
    [SerializeField]
    public float size2 = 20f; // 밀리세컨드의 크기 설정 변수

    void Start()
    {
        GameManager.instance.StartAction += () =>
        {
            StartCoroutine(CountTime());
        };
    }

    IEnumerator CountTime()
    {
        float startTime = Time.time;
        timeText.gameObject.SetActive(true);

        while (GameManager.instance.state == State.InGame)
        {
            yield return null;
            float elapsed = Time.time - startTime;
            timeText.text = TimerConvert(elapsed, size1, size2);
        }

        GameManager.instance.score = Time.time - startTime;
        Destroy(timeText.gameObject);
        Destroy(this);
    }

    public static string TimerConvert(float time, float size1, float size2)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        int milliseconds = (int)(time * 100) % 100;

        return $"<size={size1}>{minutes:00}:{seconds:00}</size><size={size2}>.{milliseconds:00}</size>";
    }
}

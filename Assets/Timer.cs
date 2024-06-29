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
            timeText.text = score.ToString();
        }

        GameManager.instance.score = score;
        timeText.gameObject.SetActive(false);
    }
}

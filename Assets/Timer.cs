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
            timeText.text = score.ToString();
        }

        GameManager.instance.score = score;
        timeText.gameObject.SetActive(false);
    }
}

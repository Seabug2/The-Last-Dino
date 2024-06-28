using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
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
        timeText.gameObject.SetActive(true);
        float startTime = Time.time;
        
        while (GameManager.instance.state.Equals(State.InGame))
        {
            float score = Time.time - startTime;
            timeText.text = score.ToString();
            yield return null;
        }

        timeText.gameObject.SetActive(false);
    }
}

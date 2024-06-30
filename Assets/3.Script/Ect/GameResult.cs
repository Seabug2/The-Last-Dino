using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameResult : MonoBehaviour
{
    [SerializeField]
    TMP_Text[] bestScoreTexts;
    [SerializeField]
    TMP_Text currentScoreText;
    [SerializeField]
    GameObject resultBoard;
    [SerializeField]
    float popupDelayTime;

    private void Awake()
    {
        resultBoard.SetActive(false);
    }

    private void Start()
    {
        GameManager.instance.GameOverAction += () =>
        {
            Invoke("RankingCheck", popupDelayTime);
        };
    }

    void RankingCheck()
    {
        int num = bestScoreTexts.Length;
        List<float> bestScores = GetBestScores(num);

        float currentScore = GameManager.instance.score;
        currentScoreText.text = Timer.TimerConvert(currentScore);

        bestScores.Add(currentScore);
        bestScores.Sort((a, b) => b.CompareTo(a)); // �������� ����
        if (bestScores.Count > num)
        {
            bestScores.RemoveAt(bestScores.Count - 1); // 4��° ���� ���� ����
        }

        print($"����� ������ {currentScore}��");

        for (int i = 0; i < num; i++)
        {
            print($"{i + 1}���� ������ {bestScores[i]}��");
            PlayerPrefs.SetFloat($"Rank{i}", bestScores[i]);
            bestScoreTexts[i].text = Timer.TimerConvert(bestScores[i]);
        }
        resultBoard.SetActive(true);
    }

    List<float> GetBestScores(int _range)
    {
        List<float> topScores = new List<float>();
        for (int i = 0; i < _range; i++)
        {
            topScores.Add(PlayerPrefs.GetFloat($"Rank{i}", 0)); // �⺻�� 0���� �ʱ�ȭ
        }
        return topScores;
    }
}

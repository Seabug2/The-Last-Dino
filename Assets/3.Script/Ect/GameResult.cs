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
        bestScores.Sort((a, b) => b.CompareTo(a)); // 내림차순 정렬
        if (bestScores.Count > num)
        {
            bestScores.RemoveAt(bestScores.Count - 1); // 4번째 이후 점수 제거
        }

        print($"당신의 점수는 {currentScore}초");

        for (int i = 0; i < num; i++)
        {
            print($"{i + 1}등의 점수는 {bestScores[i]}초");
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
            topScores.Add(PlayerPrefs.GetFloat($"Rank{i}", 0)); // 기본값 0으로 초기화
        }
        return topScores;
    }
}

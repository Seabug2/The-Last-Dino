using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameResult : MonoBehaviour
{

    float[] BestScore; // 최고 점수
    [SerializeField] TMP_Text[] TMP_BestScore; // 최고 점수를 표시할 TMP
    [SerializeField] TMP_Text TMP_NowScore; // 현재 점수를 표시할 TMP
    
    private void Start()
    {
        
        GameManager.instance.StartAction += () =>
        {
            GetComponent<GameResult>().enabled = false;
        };
        GameManager.instance.GameOverAction += () =>
        {
            GetComponent<GameResult>().enabled = true;
            StartCoroutine("Delay_co");
        };
    }

    private void _OnEnable()
    {
        BestScore = new float[4]; // 0은 미사용 (코드 읽기와 수정에 용이하도록 이렇게 하였습니다.)
                                  // 1~3은 각 인덱스의 순위 기록을 담습니다.
        float score = GameManager.instance.score;
        
        RankInput(score); // 랭킹을 계산
        for (int i = 1; i < 4; i++)
        {
            TMP_BestScore[i - 1].text = Timer.TimerConvert(BestScore[i]); // 최고 기록 3개를 나타내는 TMP에 정보 전달
        }
        TMP_NowScore.text = Timer.TimerConvert(score); // 현재 기록 전달
    }


    // 랭킹 등록용 메소드
    void RankInput(float score)
    {
        int nowRank = 4; // 일단 지금 순위는 4등
        while (nowRank > 1) // 1등 되면 반복문 끝
        {
            if (score > BestScore[nowRank - 1]) // 윗 순위를 이겼는가?
            {
                PlayerPrefs.SetFloat("Rank" + nowRank.ToString(), BestScore[nowRank - 1]); // 윗 순위 기록 끌어내리고
                nowRank--; // 지금 순위 하나 올라감
                PlayerPrefs.SetFloat("Rank" + nowRank.ToString(), score); // 올라간 자리에 현재 기록 작성
            }
            else break; // 못 이겼으면 반복문 끝
        }
    }

    private IEnumerator Delay_co()
    {
        yield return 0.3f;
        _OnEnable();
    }
}

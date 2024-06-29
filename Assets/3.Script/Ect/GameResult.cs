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
        for (int i = 1; i < 4; i++)
        {
            BestScore[i] = PlayerPrefs.GetFloat("Rank" + i.ToString()); // 저장된 최고 기록을 불러오기
        }
        int nowRank;
        for (nowRank = 3; nowRank > 0; nowRank--) // 3등부터 1등까지
        {
            if (score < BestScore[nowRank])  // i등을 못 이겼으면 반복문 끝
            {
                break;
            }
            if (nowRank < 3) // 원래 1등(2등) 기록은 2등(3등)으로 옮기기 - 3등 기록은 그냥 삭제됨
            {
                PlayerPrefs.SetFloat("Rank" + (nowRank + 1).ToString(), BestScore[nowRank]); 
            }
        }
        if (nowRank < 3) // (1~3등 이라면) 그 순위에 현재 기록 입력
        {
            PlayerPrefs.SetFloat("Rank" + (nowRank + 1).ToString(), score);
        }
    }

    private IEnumerator Delay_co()
    {
        yield return 0.3f;
        _OnEnable();
    }
}

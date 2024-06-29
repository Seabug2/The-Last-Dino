using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameResult : MonoBehaviour
{

    float[] BestScore; // �ְ� ����
    [SerializeField] TMP_Text[] TMP_BestScore; // �ְ� ������ ǥ���� TMP
    [SerializeField] TMP_Text TMP_NowScore; // ���� ������ ǥ���� TMP
    
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
        BestScore = new float[4]; // 0�� �̻�� (�ڵ� �б�� ������ �����ϵ��� �̷��� �Ͽ����ϴ�.)
                                  // 1~3�� �� �ε����� ���� ����� ����ϴ�.
        float score = GameManager.instance.score;
        
        RankInput(score); // ��ŷ�� ���
        for (int i = 1; i < 4; i++)
        {
            TMP_BestScore[i - 1].text = Timer.TimerConvert(BestScore[i]); // �ְ� ��� 3���� ��Ÿ���� TMP�� ���� ����
        }
        TMP_NowScore.text = Timer.TimerConvert(score); // ���� ��� ����
    }


    // ��ŷ ��Ͽ� �޼ҵ�
    void RankInput(float score)
    {
        int nowRank = 4; // �ϴ� ���� ������ 4��
        while (nowRank > 1) // 1�� �Ǹ� �ݺ��� ��
        {
            if (score > BestScore[nowRank - 1]) // �� ������ �̰�°�?
            {
                PlayerPrefs.SetFloat("Rank" + nowRank.ToString(), BestScore[nowRank - 1]); // �� ���� ��� �������
                nowRank--; // ���� ���� �ϳ� �ö�
                PlayerPrefs.SetFloat("Rank" + nowRank.ToString(), score); // �ö� �ڸ��� ���� ��� �ۼ�
            }
            else break; // �� �̰����� �ݺ��� ��
        }
    }

    private IEnumerator Delay_co()
    {
        yield return 0.3f;
        _OnEnable();
    }
}

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
        for (int i = 1; i < 4; i++)
        {
            BestScore[i] = PlayerPrefs.GetFloat("Rank" + i.ToString()); // ����� �ְ� ����� �ҷ�����
        }
        int nowRank;
        for (nowRank = 3; nowRank > 0; nowRank--) // 3����� 1�����
        {
            if (score < BestScore[nowRank])  // i���� �� �̰����� �ݺ��� ��
            {
                break;
            }
            if (nowRank < 3) // ���� 1��(2��) ����� 2��(3��)���� �ű�� - 3�� ����� �׳� ������
            {
                PlayerPrefs.SetFloat("Rank" + (nowRank + 1).ToString(), BestScore[nowRank]); 
            }
        }
        if (nowRank < 3) // (1~3�� �̶��) �� ������ ���� ��� �Է�
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

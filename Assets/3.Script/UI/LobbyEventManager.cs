using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyEventManager : MonoBehaviour
{
    /// <summary>
    /// 타이틀 화면에 보여줄 공룡 배열
    /// </summary>
    [SerializeField] GameObject[] dinos;

    /// <summary>
    /// 선택 중인 공룡 번호
    /// </summary>
    private int choice_number;

    private void Start()
    {
        //모든 공룡 비활성화
        foreach (GameObject g in dinos)
        {
            g.SetActive(false);
        }
        // 무작위 공룡 하나를 활성화 해둠
        choice_number = Random.Range(0, dinos.Length);
        dinos[choice_number].SetActive(true);
    }

    /// <summary>
    /// 공룡 선택창에서 좌우 화살표를 눌러 다음 공룡 선택하기
    /// </summary>
    /// <param name="num">이전 공룡 선택 = 2, 다음 공룡 선택 = 1</param>
    public void OnButton(int num)
    {
        dinos[choice_number].SetActive(false);
        choice_number += num;
        choice_number = choice_number % 3;
        dinos[choice_number].SetActive(true);
    }

    /// <summary>
    /// 게임 시작 버튼을 누르면
    /// </summary>
    public void GameStartButton()
    {
        //선택된 공룡을 제외한 나머지 공룡은 삭제
        for (int i = 0; i < dinos.Length; i++)
        {
            if (!i.Equals(choice_number))
            {
                Destroy(dinos[i]);
            }
            else
            {
                GameManager.instance.GameStart(dinos[i].transform);
            }
        }

        //지금 캔버스를 삭제
        Destroy(gameObject);
        return;
    }
}
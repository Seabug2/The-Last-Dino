using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyEventManager : MonoBehaviour
{
    /// <summary>
    /// Ÿ��Ʋ ȭ�鿡 ������ ���� �迭
    /// </summary>
    [SerializeField] GameObject[] dinos;

    /// <summary>
    /// ���� ���� ���� ��ȣ
    /// </summary>
    private int choice_number;

    private void Start()
    {
        //��� ���� ��Ȱ��ȭ
        foreach (GameObject g in dinos)
        {
            g.SetActive(false);
        }
        // ������ ���� �ϳ��� Ȱ��ȭ �ص�
        choice_number = Random.Range(0, dinos.Length);
        dinos[choice_number].SetActive(true);
    }

    /// <summary>
    /// ���� ����â���� �¿� ȭ��ǥ�� ���� ���� ���� �����ϱ�
    /// </summary>
    /// <param name="num">���� ���� ���� = 2, ���� ���� ���� = 1</param>
    public void OnButton(int num)
    {
        dinos[choice_number].SetActive(false);
        choice_number += num;
        choice_number = choice_number % 3;
        dinos[choice_number].SetActive(true);
    }

    /// <summary>
    /// ���� ���� ��ư�� ������
    /// </summary>
    public void GameStartButton()
    {
        //���õ� ������ ������ ������ ������ ����
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

        //���� ĵ������ ����
        Destroy(gameObject);
        return;
    }
}
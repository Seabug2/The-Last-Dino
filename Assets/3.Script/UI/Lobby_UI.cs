using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby_UI : MonoBehaviour
{
    [SerializeField] GameObject[] dinos;

    private int choice_number;

    private void Start()
    {
        foreach(GameObject g in dinos)
        {
            g.SetActive(false);
        }
        choice_number = Random.Range(0, dinos.Length);
        dinos[choice_number].SetActive(true);
    }

    public void OnButton(int num)
    {
        dinos[choice_number].SetActive(false);
        choice_number += num;
        choice_number = choice_number % 3;
        dinos[choice_number].SetActive(true);
    }

    public void GameStartbutton()
    {
        for(int i = 0; i < dinos.Length; i++)
        {
            if(i != choice_number)
            {
                Destroy(dinos[i]);
            }
        }
    }
}
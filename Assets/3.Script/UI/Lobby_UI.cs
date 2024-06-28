using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby_UI : MonoBehaviour
{
    [SerializeField] GameObject[] character_obj;

    private int choice_number;

    private void Start()
    {
        foreach(GameObject obj in character_obj)
        {
            obj.SetActive(false);
        }

        choice_number = Random.Range(0, character_obj.Length);
        character_obj[choice_number].SetActive(true);
    }

    public void OnButton(int number)
    {
        character_obj[choice_number].SetActive(false);
        choice_number += number;
        choice_number = choice_number % 3;
        character_obj[choice_number].SetActive(true);

        Debug.Log(choice_number);
    }
}
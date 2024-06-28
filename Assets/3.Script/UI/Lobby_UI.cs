using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby_UI : MonoBehaviour
{
    [SerializeField] Button setting_button;
    [SerializeField] Button player_choice_right_button;
    [SerializeField] Button player_choice_left_button;

    [SerializeField] GameObject[] character_obj;

    private int player_chocie_number = 1;

    private void Awake()
    {
        setting_button = setting_button.gameObject.GetComponent<Button>();
        player_choice_right_button = player_choice_right_button.gameObject.GetComponent<Button>();
        player_choice_left_button = player_choice_left_button.gameObject.GetComponent<Button>();
    }

    private void Start()
    {
        setting_button.onClick.AddListener(Setting_button);
        player_choice_right_button.onClick.AddListener(Player_choice_right_button);
        // player_choice_left_button.onClick.AddListener(Player_choice_left_button);
    }

    private void Setting_button()
    {
        Debug.Log("설정 버튼 클릭!");
    }

    private void Player_choice_right_button()
    {
        player_chocie_number += 1;

        if (player_chocie_number < 4)
        {
            if (player_chocie_number == 1)
            {
                character_obj[0].SetActive(true);
                character_obj[2].SetActive(false);

                Debug.Log(player_chocie_number);
            }

            if (player_chocie_number == 2)
            {
                character_obj[0].SetActive(false);
                character_obj[1].SetActive(true);

                Debug.Log(player_chocie_number);
            }

            if (player_chocie_number == 3)
            {
                character_obj[1].SetActive(false);
                character_obj[2].SetActive(true);

                Debug.Log(player_chocie_number);
            }
        }
        else
        {
            player_chocie_number = 1;

            character_obj[0].SetActive(true);
            character_obj[2].SetActive(false);
        }
    }
}

    /* private void Player_choice_left_button()
    {
        player_chocie_number -= 1;
    } */
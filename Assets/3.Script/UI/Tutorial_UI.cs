using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_UI : MonoBehaviour
{
    [SerializeField] Button setting_button; // 설정 비활성화
    [SerializeField] Button right_button; // 오른쪽 버튼
    [SerializeField] Button left_button; // 왼쪽 버튼

    [SerializeField] GameObject[] text_objs; // 텍스트
    [SerializeField] GameObject tutorial_obj; // 팝업 활성화

    bool on_off = false;

    public void On_click_setting() // 팝업 ON & OFF 메소드
    {
        on_off = !on_off;

        tutorial_obj.SetActive(on_off);

        setting_button.interactable = !on_off;
    }

    public void On_click_button(int value) // 버튼, 텍스트 ON & OFF 메소드
    {
        switch (value)
        {
            case 0:

                left_button.interactable = false;
                right_button.interactable = true;

                text_objs[0].SetActive(true);
                text_objs[1].SetActive(false);

                break;

            case 1:

                right_button.interactable = false;
                left_button.interactable = true;

                text_objs[0].SetActive(false);
                text_objs[1].SetActive(true);

                break;
        }
    }
}
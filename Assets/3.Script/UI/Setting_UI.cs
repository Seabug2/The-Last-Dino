using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting_UI : MonoBehaviour
{
    [SerializeField] Button tutorial_button;

    /// <summary>
    /// �Ѱ� ���� ���� ���
    /// </summary>
    [SerializeField] GameObject setting_obj;

    bool on_off = false;

    public void On_click_setting()
    {
        setting_obj.SetActive(!setting_obj.activeSelf);
        tutorial_button.interactable = !tutorial_button.interactable;
    }
}
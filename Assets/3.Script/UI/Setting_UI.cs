using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting_UI : MonoBehaviour
{
    [SerializeField] Button tutorial_button;

    [SerializeField] GameObject setting_obj;

    bool on_off = false;

    public void On_click_setting()
    {
        on_off = !on_off;

        setting_obj.SetActive(on_off);

        tutorial_button.interactable = !on_off;
    }
}
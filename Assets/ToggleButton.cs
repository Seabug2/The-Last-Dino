using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    public GameObject[] myArray;
    public GameObject[] targetArray;

    public void OnOffTargets()
    {
        foreach(GameObject g in myArray)
        {
            g.SetActive(!g.activeSelf);
        }
        foreach(GameObject g in targetArray)
        {
            g.SetActive(false);
        }
    }
}

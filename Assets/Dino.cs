using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : MonoBehaviour
{
    public Transform propellerPosition;
    public Transform umbrellaPosition;
    public  GameObject iTemPrtc;

    public void GetItem()
    {
        iTemPrtc.SetActive(true);
    }

    
}

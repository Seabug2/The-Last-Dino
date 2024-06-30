using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Meat,
    Propeller,
    Umbrella
}
public class Item : MonoBehaviour
{
    public ItemType type;

    private void OnEnable()
    {
        Invoke("SetUnactive", 10f);
    }
    private void OnDisable()
    {
        CancelInvoke("SetUnactive");
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Dino>().GetItem(type);
        SetUnactive();
    }

    void SetUnactive()
    {
        gameObject.SetActive(false);
    }
}

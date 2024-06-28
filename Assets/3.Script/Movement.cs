using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    DinoData data;
    [SerializeField]
    float speed;
    [SerializeField]
    float anglerSpeed;

    private void OnEnable()
    {
        speed = data.speed;
        anglerSpeed = data.angluerSpeed;
    }

    void FixedUpdate()
    {
        transform.rotation *= Quaternion.Euler(Time.fixedDeltaTime * speed, Input.GetAxis("Horizontal") * anglerSpeed, 0);
    }
}

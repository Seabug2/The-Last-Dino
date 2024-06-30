using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    DinoData data;

    public DinoData Data { get { return data; } }
    
    [SerializeField]
    float speed = 0;
    [SerializeField]
    float anglerSpeed = 0;

    const float offset = 25;

    public void SetData(DinoData _DinoData)
    {
        data = _DinoData;
        speed = data.speed;
        anglerSpeed = data.angluerSpeed;
    }

    void FixedUpdate()
    {
        transform.rotation *= Quaternion.Euler(Time.fixedDeltaTime * speed * offset, Input.GetAxis("Horizontal") * anglerSpeed, 0);
    }
}

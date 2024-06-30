using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAround : MonoBehaviour
{
    [SerializeField]
    float speed;
    private void FixedUpdate()
    {
        transform.rotation *= Quaternion.Euler(0,Time.fixedDeltaTime * speed, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.rotation *= Quaternion.Euler(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), 0);
    }
}

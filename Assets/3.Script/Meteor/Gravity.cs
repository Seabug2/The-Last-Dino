using UnityEngine;

// 오브젝트에 리지드 바디가 필수적으로 요구됩니다.
[RequireComponent(typeof(Rigidbody))]
public class Gravity : MonoBehaviour
{
    /// <summary>
    /// 운석이 떨어지는 속도
    /// </summary>
    public float speed;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Stop()
    {
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        this.enabled = false;
    }

    private void FixedUpdate()
    {
        rb.AddForce((Vector3.zero - transform.position).normalized * speed, ForceMode.Acceleration);
    }
}

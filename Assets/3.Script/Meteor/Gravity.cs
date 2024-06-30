using UnityEngine;

// ������Ʈ�� ������ �ٵ� �ʼ������� �䱸�˴ϴ�.
[RequireComponent(typeof(Rigidbody))]
public class Gravity : MonoBehaviour
{
    /// <summary>
    /// ��� �������� �ӵ�
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

using UnityEngine;

// ������Ʈ�� ������ �ٵ� �ʼ������� �䱸�˴ϴ�.
[RequireComponent(typeof(Rigidbody))]
public class Meteor : MonoBehaviour
{
    Rigidbody rb;

    /// <summary>
    /// x = ��鸲 ũ��
    /// y = ��鸲 �ð�
    /// </summary>
    public Vector2 shakeScale = Vector2.one;

    const float impactRange = 18f;

    /// <summary>
    /// ��� �������� �ӵ�
    /// </summary>
    public float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    [SerializeField]
    GameObject prtc;

    /// <summary>
    /// ��� �ε��� �� �ִ� ������Ʈ�� ����� �����Դϴ�.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        // ī�޶� ����ũ
        CameraController camCtrl = Camera.main.GetComponent<CameraController>();

        if (!Physics.OverlapSphere(other.ClosestPoint(transform.position), impactRange, 1 << LayerMask.NameToLayer("Dino")).Length.Equals(0))
        {
            shakeScale *= 0.5f;
            camCtrl.StartShakeCam(shakeScale.x, shakeScale.y);
        }

        GameObject p = Instantiate(prtc, transform.position, Quaternion.identity);
        p.transform.up = transform.position.normalized;

        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        rb.AddForce((Vector3.zero - transform.position).normalized * speed, ForceMode.Acceleration);
    }
}

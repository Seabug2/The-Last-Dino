using UnityEngine;

// ������Ʈ�� ������ �ٵ� �ʼ������� �䱸�˴ϴ�.
[RequireComponent(typeof(Rigidbody))]
public class Meteor : MonoBehaviour
{
    Rigidbody rb;

    /// <summary>
    /// x = �ּ� ũ��
    /// y = �ִ� ũ��
    /// </summary>
    public Vector2 sizeRange = Vector2.one;

    /// <summary>
    /// x = ��鸲 ũ��
    /// y = ��鸲 �ð�
    /// </summary>
    public Vector2 shakeScale = Vector2.one;

    const float impactRange = 15f;

    /// <summary>
    /// ��� �������� �ӵ�
    /// </summary>
    public float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Ȱ��ȭ�� �Ǹ� �׻� velocity�� �ʱ�ȭ �մϴ�.
    /// </summary>
    private void OnEnable()
    {
        rb.velocity = Vector3.zero;
    }

    // ��� �ε��� �� �ִ� ������Ʈ�� ����� �����Դϴ�.
    private void OnTriggerEnter(Collider other)
    {
        // ī�޶� ����ũ
        CameraController camCtrl = Camera.main.GetComponent<CameraController>();

        //����� �ε�����
        if (other.CompareTag("Dino"))
        {
            camCtrl.StartShakeCam(shakeScale.x, shakeScale.y);
        }

        //���� �ε�����
        else
        {
            if (!Physics.OverlapSphere(other.ClosestPoint(transform.position), impactRange, 1 << LayerMask.NameToLayer("Dino")).Length.Equals(0))
            {
                shakeScale *= 0.5f;
                camCtrl.StartShakeCam(shakeScale.x, shakeScale.y);
            }

            //ũ������ ����

        }

        // �浹 �̺�Ʈ�� �����ϰ� ��Ȱ��ȭ �մϴ�.
        gameObject.SetActive(false);
    }


    private void FixedUpdate()
    {
        // ������ �߽��� ���� �̵��մϴ�.
        rb.AddForce((Vector3.zero - transform.position).normalized * speed, ForceMode.Acceleration);
    }
}

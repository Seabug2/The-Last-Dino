using UnityEngine;

// 오브젝트에 리지드 바디가 필수적으로 요구됩니다.
[RequireComponent(typeof(Rigidbody))]
public class Meteor : MonoBehaviour
{
    Rigidbody rb;

    /// <summary>
    /// x = 흔들림 크기
    /// y = 흔들림 시간
    /// </summary>
    public Vector2 shakeScale = Vector2.one;

    const float impactRange = 18f;

    /// <summary>
    /// 운석이 떨어지는 속도
    /// </summary>
    public float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    [SerializeField]
    GameObject prtc;

    /// <summary>
    /// 운석과 부딪힐 수 있는 오브젝트는 공룡과 지구입니다.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        // 카메라 쉐이크
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

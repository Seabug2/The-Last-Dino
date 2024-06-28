using UnityEngine;

// 오브젝트에 리지드 바디가 필수적으로 요구됩니다.
[RequireComponent(typeof(Rigidbody))]
public class Meteor : MonoBehaviour
{
    Rigidbody rb;

    /// <summary>
    /// x = 최소 크기
    /// y = 최대 크기
    /// </summary>
    public Vector2 sizeRange = Vector2.one;

    /// <summary>
    /// x = 흔들림 크기
    /// y = 흔들림 시간
    /// </summary>
    public Vector2 shakeScale = Vector2.one;

    const float impactRange = 15f;

    /// <summary>
    /// 운석이 떨어지는 속도
    /// </summary>
    public float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 활성화가 되면 항상 velocity를 초기화 합니다.
    /// </summary>
    private void OnEnable()
    {
        rb.velocity = Vector3.zero;
    }

    // 운석과 부딪힐 수 있는 오브젝트는 공룡과 지구입니다.
    private void OnTriggerEnter(Collider other)
    {
        // 카메라 쉐이크
        CameraController camCtrl = Camera.main.GetComponent<CameraController>();

        //공룡과 부딪히면
        if (other.CompareTag("Dino"))
        {
            camCtrl.StartShakeCam(shakeScale.x, shakeScale.y);
        }

        //땅에 부딪히면
        else
        {
            if (!Physics.OverlapSphere(other.ClosestPoint(transform.position), impactRange, 1 << LayerMask.NameToLayer("Dino")).Length.Equals(0))
            {
                shakeScale *= 0.5f;
                camCtrl.StartShakeCam(shakeScale.x, shakeScale.y);
            }

            //크레이터 생성

        }

        // 충돌 이벤트를 실행하고 비활성화 합니다.
        gameObject.SetActive(false);
    }


    private void FixedUpdate()
    {
        // 지구의 중심을 향해 이동합니다.
        rb.AddForce((Vector3.zero - transform.position).normalized * speed, ForceMode.Acceleration);
    }
}

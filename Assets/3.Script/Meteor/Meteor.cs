using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Gravity))]
public class Meteor : MonoBehaviour
{
    // 운석은 생성시 지구 (0,0,0)을 향해 이동한다
    // 운석은 충돌 지역에 경고 마크를 띄운다
    // 플레이어가 지구와 충돌시 게임이 끝난다
    // 운석이 지구에 부딪히면 땅에 박히고
    // 시간이 흐르면 폭발한다
    // 폭발시 폭발 파티클을 생성한다.
    // 플레이어가 폭발에 휘말리면 게임이 끝난다

    /// <summary>
    /// x = 흔들림 크기
    /// y = 흔들림 시간
    /// </summary>
    [Tooltip("x = 흔들림 크기, y = 흔들림 시간")]
    public Vector2 shakeScale = Vector2.one;

    [SerializeField]
    float impactRange = 25f;
    [SerializeField]
    float explosionRange = 10;
    [SerializeField]
    float delayTime = 30f;

    public GameObject collisionPointMark;
    public GameObject tails;
    public GameObject explosion;

    private void Start()
    {
        GameObject go = Instantiate(collisionPointMark);

        transform.forward = (Vector3.zero - transform.position).normalized;

        Ray ray = new Ray(transform.position, (Vector3.zero - transform.position).normalized);
        if(Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, 100f, 1 << LayerMask.NameToLayer("Earth")))
        {
            go.transform.position = (transform.position).normalized * 43.9f;
        }
        go.transform.up = (transform.position).normalized;
        collisionPointMark = go;
    }

    /// <summary>
    /// 운석과 부딪힐 수 있는 오브젝트는 공룡과 지구입니다.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dino"))
        {
            GameManager.instance.GameOver(other.gameObject);
            Explosion();
        }
        else
        {
            //지구와 부딪힌 경우 잠시 후 폭발
            StartCoroutine(DelayedExplosion_co());
        }

        NearGroundImpact();
        Destroy(collisionPointMark);
    }

    void NearGroundImpact()
    {
        CameraController camCtrl = Camera.main.GetComponent<CameraController>();
        if (!Physics.OverlapSphere(transform.position, impactRange, 1 << LayerMask.NameToLayer("Dino")).Length.Equals(0))
        {
            camCtrl.StartShakeCam(shakeScale.x, shakeScale.y);
        }
    }

    void Explosion()
    {
        GameObject p = Instantiate(explosion);
        p.transform.position = transform.position.normalized * 44.5f;
        p.transform.up = transform.position.normalized;
        Destroy(gameObject);
    }

    IEnumerator DelayedExplosion_co()
    {
        GetComponent<Gravity>().Stop();

        // 지정된 시간 후에 폭발
        yield return new WaitForSeconds(delayTime);
        Explosion();
    }

    Vector3 vec = Vector3.zero;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, impactRange);
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}

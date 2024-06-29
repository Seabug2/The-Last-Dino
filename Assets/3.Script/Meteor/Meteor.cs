using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Gravity))]
public class Meteor : MonoBehaviour
{
    // ��� ������ ���� (0,0,0)�� ���� �̵��Ѵ�
    // ��� �浹 ������ ��� ��ũ�� ����
    // �÷��̾ ������ �浹�� ������ ������
    // ��� ������ �ε����� ���� ������
    // �ð��� �帣�� �����Ѵ�
    // ���߽� ���� ��ƼŬ�� �����Ѵ�.
    // �÷��̾ ���߿� �ָ����� ������ ������

    /// <summary>
    /// x = ��鸲 ũ��
    /// y = ��鸲 �ð�
    /// </summary>
    [Tooltip("x = ��鸲 ũ��, y = ��鸲 �ð�")]
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
    /// ��� �ε��� �� �ִ� ������Ʈ�� ����� �����Դϴ�.
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
            //������ �ε��� ��� ��� �� ����
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

        // ������ �ð� �Ŀ� ����
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

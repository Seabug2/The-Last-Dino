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

    public GameObject explosion;
    public GameObject collisionPointMark;

    Gravity gravity;
    private void Awake()
    {
        gravity = GetComponent<Gravity>();
    }

    private void OnEnable()
    {
        transform.forward = (Vector3.zero - transform.position).normalized;
        gravity.Init();
        Ray ray = new Ray(transform.position, (Vector3.zero - transform.position).normalized);
        if(Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, 100f, 1 << LayerMask.NameToLayer("Earth")))
        {
            collisionPointMark.transform.position = (transform.position).normalized * 45f;
        }
        collisionPointMark.transform.up = (transform.position).normalized;
        collisionPointMark.SetActive(true);
        collisionPointMark.transform.SetParent(null);
    }

    /// <summary>
    /// ��� �ε��� �� �ִ� ������Ʈ�� ����� �����Դϴ�.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dino"))
        {
            Explosion();
        }
        else
        {
            //������ �ε��� ��� ��� �� ����
            StartCoroutine(DelayedExplosion_co());
        }

        NearGroundImpact();
        collisionPointMark.SetActive(false);
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
        p.transform.position = transform.position.normalized * 44.8f;
        p.transform.up = transform.position.normalized;
        
        //���� ���� ���� ĳ���Ͱ� ������ ���ӿ���
        CameraController camCtrl = Camera.main.GetComponent<CameraController>();

        Collider[] cols = Physics.OverlapSphere(transform.position, explosionRange, 1 << LayerMask.NameToLayer("Dino"));

        if (cols.Length > 0)
        {
            camCtrl.StartShakeCam(shakeScale.x, shakeScale.y);
            GameManager.instance.GameOver(cols[0].transform);
        }

        gameObject.SetActive(false);
    }

    IEnumerator DelayedExplosion_co()
    {
        gravity.Stop();
        float rand = Random.Range(.5f, 1f);
        // ������ �ð� �Ŀ� ����
        yield return new WaitForSeconds(delayTime * rand);
        Explosion();
    }

    Vector3 vec = Vector3.zero;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, impactRange);
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}

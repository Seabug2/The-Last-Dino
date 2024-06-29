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

    const float impactRange = 25f;
    const float delayTime = 10f;

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
            go.transform.position = hit.point;
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
            Explosion(other.ClosestPoint(transform.position));
        }
        else
        {
            //������ �ε��� ��� ��� �� ����
            StartCoroutine(DelayedExplosion_co());
        }

        Destroy(collisionPointMark);
    }

    void NearGroundImpact(Vector3 _collisionPoint)
    {
        CameraController camCtrl = Camera.main.GetComponent<CameraController>();
        if (!Physics.OverlapSphere(_collisionPoint, impactRange, 1 << LayerMask.NameToLayer("Dino")).Length.Equals(0))
        {
            camCtrl.StartShakeCam(shakeScale.x, shakeScale.y);
        }
    }

    void Explosion(Vector3 _collisionPoint)
    {
        GameObject p = Instantiate(explosion, transform.position, Quaternion.identity);
        p.transform.up = transform.position.normalized;

        NearGroundImpact(_collisionPoint);

        Destroy(gameObject);
    }

    IEnumerator DelayedExplosion_co()
    {
        GetComponent<Gravity>().Stop();

        // ������ �ð� �Ŀ� ����
        yield return new WaitForSeconds(delayTime);
        Explosion(transform.position);
    }
}

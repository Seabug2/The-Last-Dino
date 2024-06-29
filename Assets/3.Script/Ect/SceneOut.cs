using UnityEngine;

public class SceneOut : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public float frequency = 1.0f; // ���� ���ļ� (�ʴ� �� �� ��������)
    public float amplitude = 1.0f; // ������ ���� (�ִ� �̵� �Ÿ�)

    private Vector3 startPosition;

    private void Start()
    {
        // ��ü�� �ʱ� ��ġ�� ����
        startPosition = transform.position;
    }

    private void Update()
    {
        // �ð��� ���� ���� ��� ����Ͽ� Y�� ��ġ�� ����
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = startPosition + new Vector3(0, yOffset, 0);
    }
}

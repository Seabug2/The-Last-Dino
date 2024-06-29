using UnityEngine;

public class SceneOut : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public float frequency = 1.0f; // 진동 주파수 (초당 몇 번 움직일지)
    public float amplitude = 1.0f; // 진동의 진폭 (최대 이동 거리)

    private Vector3 startPosition;

    private void Start()
    {
        // 객체의 초기 위치를 저장
        startPosition = transform.position;
    }

    private void Update()
    {
        // 시간에 따라 사인 곡선을 계산하여 Y축 위치를 변경
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = startPosition + new Vector3(0, yOffset, 0);
    }
}

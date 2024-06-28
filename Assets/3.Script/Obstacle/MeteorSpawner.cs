using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    /// <summary>
    /// � ������
    /// </summary>
    [SerializeField] private GameObject meteor;
    // � ���� ����
    [SerializeField] private float SpawnTime;

    /// <summary>
    /// ���� ���� ������ ��� �ִ� ����
    /// </summary>
    [SerializeField] private int maxPoolingCount = 10;

    // ť
    private Queue<GameObject> q_meteor;

    private Vector3 PoolingPosition;

    [SerializeField]
    float respawnHeight = 1;

    /// <summary>
    /// � Dequeue
    /// </summary>
    /// <param name="position"></param>
    void Meteor_Enable(Vector3 position)
    {
        GameObject m;

        // ť�� �� �̻� ��� ���� �� ���� ������ � ���� ���� �������� ���ο� ��� �����Ѵ�.
        if (q_meteor.Count.Equals(0) && maxPoolingCount > 0)
        {
            m = Instantiate(meteor);
            // ��� �̺�Ʈ�� �ʿ��� �޼��带 �߰��ؾ��մϴ�.
        }
        else
        {
            // ť�� ��� ���������Ƿ� �״�� ���� ���� �˴ϴ�.
            m = q_meteor.Dequeue();
        }
        
        //��� ������ ��ġ�� �����մϴ�.
        m.transform.position = Random.onUnitSphere * respawnHeight;

        //��� ��ġ�� ����������
        m.SetActive(true);
    }

    private void StartMeteorSpawn()
    {
        if(SpawnMeteor != null)
        {
            StopCoroutine(SpawnMeteor);
        }
        SpawnMeteor = StartCoroutine(MeteorSpawn_co());
    }

    Coroutine SpawnMeteor;

    private IEnumerator MeteorSpawn_co()
    {   
        // ť�� �ʱ�ȭ�� �� ��� ����
        q_meteor = new Queue<GameObject>();
        WaitForSeconds wfs = new WaitForSeconds(SpawnTime);
        if(q_meteor.Count>0)
        {
            while(true)
            {
                Vector3 meteorPosition = Random.onUnitSphere * 10f;
                Vector3 position = new Vector3(meteorPosition.x, meteorPosition.y,0);
                Meteor_Enable(position);
                yield return wfs;
            }
        }
    }

}

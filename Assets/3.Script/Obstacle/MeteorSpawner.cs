using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    /// <summary>
    /// 운석 프리팹
    /// </summary>
    [SerializeField] private GameObject meteor;
    // 운석 생성 간격
    [SerializeField] private float SpawnTime;

    /// <summary>
    /// 동시 생성 가능한 운석의 최대 개수
    /// </summary>
    [SerializeField] private int maxPoolingCount = 10;

    // 큐
    private Queue<GameObject> q_meteor;

    private Vector3 PoolingPosition;

    [SerializeField]
    float respawnHeight = 1;

    /// <summary>
    /// 운석 Dequeue
    /// </summary>
    /// <param name="position"></param>
    void Meteor_Enable(Vector3 position)
    {
        GameObject m;

        // 큐에 더 이상 운석이 없을 때 생성 가능한 운석 수가 아직 남았으면 새로운 운석을 생성한다.
        if (q_meteor.Count.Equals(0) && maxPoolingCount > 0)
        {
            m = Instantiate(meteor);
            // 운석의 이벤트에 필요한 메서드를 추가해야합니다.
        }
        else
        {
            // 큐에 운석이 남아있으므로 그대로 꺼내 쓰면 됩니다.
            m = q_meteor.Dequeue();
        }
        
        //운석을 랜덤한 위치에 생성합니다.
        m.transform.position = Random.onUnitSphere * respawnHeight;

        //운석의 위치가 정해졌으면
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
        // 큐를 초기화한 후 운석을 생성
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

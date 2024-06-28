using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner: MonoBehaviour
{
    [SerializeField] private GameObject MeteorPrefabs;
    [SerializeField] private float SpawnTime;

    private Queue<GameObject> meteor_q;
    private Vector3 PoolingPosition;
    [SerializeField] private int Meteor_PoolCount = 30;


    private void Awake()
    {
        meteor_q = new Queue<GameObject>();
        PoolingPosition = new Vector3(0, 100, 0);

        for(int i=0;i<Meteor_PoolCount;i++)
        {
            GameObject meteor =
                Instantiate(MeteorPrefabs, PoolingPosition, Quaternion.identity);
            meteor.SetActive(false);
            meteor_q.Enqueue(meteor);
        }
    }

    private void Start()
    {
        if(SpawnMeteor != null)
        {
            StopCoroutine(SpawnMeteor);
        }

        SpawnMeteor = StartCoroutine(MeteorSpawn_co());
    }
    Coroutine SpawnMeteor;
    public void Meteor_Enable(Vector3 position)
    {
        GameObject Enemy = meteor_q.Dequeue();
        Enemy.transform.position = position;
        if (!Enemy.activeSelf)
            Enemy.SetActive(true);
       

    }
    public void Meteor_Disable(GameObject meteor)
    {
        if(meteor.activeSelf)
        {
            meteor.SetActive(false);
        }
        meteor.transform.position = PoolingPosition;
        meteor_q.Enqueue(meteor);
    }

    private IEnumerator MeteorSpawn_co()
    {
        WaitForSeconds wfs = new WaitForSeconds(SpawnTime);
        if(meteor_q.Count>0)
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

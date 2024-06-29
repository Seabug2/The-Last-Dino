using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject meat, propeller, umbrella;
    [SerializeField] private float delayTime = 10f;

    int count = 3;
    private SphereCollider area;
    private List<GameObject> ItemList = new List<GameObject>();
    
    private void Start()
    {
        area = GetComponent<SphereCollider>();
        ItemList.Add(meat);
        ItemList.Add(propeller);
        ItemList.Add(umbrella);
    }
    private void Update()
    {
        if(itemSpawn !=null)
        {
            StopCoroutine(ItemSpawner_co(delayTime));
        }
         itemSpawn = StartCoroutine(ItemSpawner_co(delayTime));
    }
    Coroutine itemSpawn;

    private IEnumerator ItemSpawner_co(float delayTime)
    {
        for(int i = 0;i<count;i++)
        {
            // Vector3 spawnPos =;
            // GameObject instance = Instantiate(ItemList,spawnPos,Quaternion.identity)

            Vector3 randomPos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized*43.5f;
            Instantiate(ItemList[Random.Range(0,ItemList.Count)], randomPos, Quaternion.identity);
        }

        yield return new WaitForSeconds(delayTime);
    }
    
}

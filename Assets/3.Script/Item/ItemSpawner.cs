using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject meat, propeller, umbrella;
    private bool isMeat = false;
    private bool ispropeller = false;
    private bool isumbrella = false;

    int count = 3;
    private SphereCollider area;
    private List<GameObject> ItemList = new List<GameObject>();
    
    private void Start()
    {
        area = GetComponent<SphereCollider>();
        //코루틴 시작
        ItemList.Add(meat);
        ItemList.Add(propeller);
        ItemList.Add(umbrella);
    }

    private IEnumerator (float delayTime)
    {
        for(int i = 0;i<count;i++)
        {
            // Vector3 spawnPos =;
            // GameObject instance = Instantiate(ItemList,spawnPos,Quaternion.identity)

            Vector3 randomPos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized*43.5f;
            GameObject instance = Instantiate(ItemList[Random.Range(0,ItemList.Count)], randomPos, Quaternion.identity);
        }
    }
}

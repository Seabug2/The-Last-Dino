using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject meat, propeller, umbrella;
    [SerializeField] private float delayTime = 10f;

    int count = 3;
    [SerializeField] private float respawnPos = 1;
    [SerializeField] private float detectionRadius;
    private List<GameObject> ItemList = new List<GameObject>();
    
    private void Start()
    {
        
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
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        
        Collider[] hitColliders = Physics.OverlapSphere(ray.origin, detectionRadius, 2 << LayerMask.NameToLayer("Meteor"));

        if (hitColliders.Length > 0)
        {

            for (int i = 0;i<count;i++)
            {
                    //Physics.OverlapSphere
                    Vector3 randomPos = Random.onUnitSphere * respawnPos;
                    //Vector3 randomPos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized*43.5f;
                    Instantiate(ItemList[Random.Range(0,ItemList.Count)], randomPos, Quaternion.identity);
            }
        }
        yield return new WaitForSeconds(delayTime);
    }
    
}

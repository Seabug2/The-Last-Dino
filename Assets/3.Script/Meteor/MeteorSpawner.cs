using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject meteor;
    List<GameObject> meteorList = new List<GameObject>();
    [SerializeField] private float SpawnTime;

    [SerializeField]
    float respawnHeight = 1;

    private void Start()
    {
        GameManager.instance.StartAction += () =>
        {
            StartCoroutine("MeteorSpawn_co");
        };

        GameManager.instance.GameOverAction += () =>
        {
            StopCoroutine("MeteorSpawn_co");
            Destroy(this);
        };
    }

    private IEnumerator MeteorSpawn_co()
    {
        GameObject m;
        WaitForSeconds wfs = new WaitForSeconds(SpawnTime);
        while (GameManager.instance.state.Equals(State.InGame))
        {
            yield return wfs;

            if (meteorList.Count.Equals(0))
            {
                m = InstanceMeteor(); 
            }
            else
            {
                m = meteorList[0];
                meteorList.Remove(m);
            }

            Vector3 respawnPoint = Random.onUnitSphere * respawnHeight;
            Ray ray = new Ray(respawnPoint, (Vector3.zero - respawnPoint).normalized);
            
            if (Physics.Raycast(ray.origin, ray.direction, Mathf.Infinity, 1 << LayerMask.NameToLayer("Meteor")))
            {
                continue;
            }

            m.transform.position = respawnPoint;
            m.SetActive(true);
        }
    }

    GameObject InstanceMeteor()
    {
        GameObject go = Instantiate(meteor);
        go.GetComponent<ReturnList>().SetMyList(this.meteorList);
        return go;
    }
}

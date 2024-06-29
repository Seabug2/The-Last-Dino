using System.Collections;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject meteor;
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
        WaitForSeconds wfs = new WaitForSeconds(SpawnTime);
        while (GameManager.instance.state.Equals(State.InGame))
        {
            Vector3 respawnPoint = Random.onUnitSphere * respawnHeight;
            Ray ray = new Ray(respawnPoint, (Vector3.zero - respawnPoint).normalized);
            
            if (Physics.Raycast(ray.origin, ray.direction, Mathf.Infinity, 1 << LayerMask.NameToLayer("Meteor")))
            {
                continue;
            }

            Instantiate(meteor, respawnPoint, Quaternion.identity);
            yield return wfs;
        }
    }
}

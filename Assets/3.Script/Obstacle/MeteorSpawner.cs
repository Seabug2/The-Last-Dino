using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject meteor;
    // � ���� ����
    [SerializeField] private float SpawnTime;

    [SerializeField]
    float respawnHeight = 1;

    private void Start()
    {
        GameManager.instance.StartAction += () =>
        {
            StartCoroutine(MeteorSpawn_co());
        };
    }

    private IEnumerator MeteorSpawn_co()
    {
        WaitForSeconds wfs = new WaitForSeconds(SpawnTime);
        while (GameManager.instance.state.Equals(State.InGame))
        {
            Instantiate(meteor, Random.onUnitSphere * respawnHeight, Quaternion.identity);
            yield return wfs;
        }
    }
}

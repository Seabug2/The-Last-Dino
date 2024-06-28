using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner: MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefabs;
    [SerializeField] private float SpawnTime;

    private Queue<GameObject> Meteor_q;
    private Vector3 PoolingPosition;
    [SerializeField] private int Enemy_PoolCount = 30;


    private void Awake()
    {
        Meteor_q = new Queue<GameObject>();
        PoolingPosition = new Vector3(0, 0, 0);
    }
}

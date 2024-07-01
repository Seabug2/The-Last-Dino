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

    /// <summary>
    /// 화면 가로
    /// </summary>
    [SerializeField] int screenwidth, xOffset;
    /// <summary>
    /// 화면 높이
    /// </summary>
    [SerializeField] int screenHeight, yOffset;
    /// <summary>
    /// 운석을 생성할 거리
    /// </summary>
    [SerializeField] float distanceFromCamera = 10f;

    private void Start()
    {
        screenwidth = Screen.width;
        screenHeight = Screen.height;

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

    public LayerMask layersToCheck;

    private IEnumerator MeteorSpawn_co()
    {
        GameObject m;
        while (GameManager.instance.state.Equals(State.InGame))
        {
            yield return new WaitForSeconds(SpawnTime * Random.Range(.57f,1.2f));

            if (meteorList.Count.Equals(0))
            {
                m = InstanceMeteor(); 
            }
            else
            {
                m = meteorList[0];
                meteorList.Remove(m);
            }

            Ray ray;
            Vector3 respawnPoint;
            do
            {
                respawnPoint = GetRespawnPosition();
                ray = new Ray(respawnPoint, (Vector3.zero - respawnPoint).normalized);
            }
            while (Physics.Raycast(ray.origin, ray.direction, Mathf.Infinity, layersToCheck));

            m.transform.position = respawnPoint;
            m.SetActive(true);

            ////운석 두 개씩 생성
            //if (!meteorList.Count.Equals(0))
            //{
            //    m = meteorList[0];
            //    meteorList.Remove(m);
            //}
            //else { continue; }

            //do
            //{
            //    respawnPoint = Random.onUnitSphere * respawnHeight;
            //    ray = new Ray(respawnPoint, (Vector3.zero - respawnPoint).normalized);
            //}
            //while (Physics.Raycast(ray.origin, ray.direction, Mathf.Infinity, 1 << LayerMask.NameToLayer("Meteor")));

            //m.transform.position = respawnPoint;
            //m.SetActive(true);
        }
    }

    GameObject InstanceMeteor()
    {
        GameObject go = Instantiate(meteor);
        go.GetComponent<ReturnList>().SetMyList(this.meteorList);
        return go;
    }

    Vector3 GetRespawnPosition()
    {
        int randX = Random.Range(xOffset, screenwidth - xOffset);
        int randY = screenHeight + yOffset;

        // RandomScreenPosition
        Vector2 rsp = new Vector2(randX, randY);
        Ray ray = Camera.main.ScreenPointToRay(rsp);

        float rand = Random.Range(.8f, 1.2f);
        Vector3 rayPoint = ray.GetPoint(distanceFromCamera + rand);

        return rayPoint;
    }
}

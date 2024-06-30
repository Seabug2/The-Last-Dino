using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //* 아이템 스포너를 아이템 매니저로 새로 만들어주세요

    //* 아이템 생성 주기 float 변수
    public float respawnTime = 10f;
    //아이템 리스트
    // 만들어주세요
    public List<GameObject> itemList = new List<GameObject>();

    /// <summary>
    /// 아이템을 생성할 때 이 범위를 검사합니다.
    /// </summary>
    public float spwnRadius = 10;

    private void Start()
    {
        //아이템을 먼저 비활성화 해둡니다.
        for(int i = 0; i < itemList.Count; i ++)
        {
            itemList[i].GetComponent<ReturnList>().SetMyList(itemList);
            itemList[i].SetActive(false);
        }

        //초기화 단계에서 게임매니저의 액션에 필요한 코루틴을 등록합니다.
        GameManager.instance.StartAction += () =>
        {
            StartCoroutine("AppearItem_co");
        };

        GameManager.instance.GameOverAction += () =>
        {
            StopCoroutine("AppearItem_co");
            Destroy(this);
        };
    }


    public LayerMask layersToCheck;


    private IEnumerator AppearItem_co()
    {
        int originCount = itemList.Count;

        //// 검출할 다중 레이어를 만드는 방법
        //int meteorLayer = LayerMask.NameToLayer("Meteor");
        //int playerLayer = LayerMask.NameToLayer("Dino");
        ////레이어 계산은 비트연산을 통해 이루어집니다.
        //int combinedLayerMask = (1 << meteorLayer) | (1 << playerLayer);

        //랜덤한 위치를 저장할 변수를 캐싱해둡니다.
        Vector3 respawnPoint;

        while (GameManager.instance.state.Equals(State.InGame))
        {
            //respawnTime 시간 후에 아이템 생성을 할 수 있습니다.
            yield return new WaitForSeconds(respawnTime * Random.Range(.7f,1.2f));

            //아이템을 생성하려고 할 때 리스트 안에 아이템이 전부 모여있지 않으면 아이템 생성을 하지 않습니다.
            if (!itemList.Count.Equals(originCount)) continue;

            //랜덤한 위치를 구합니다.
            do {
                respawnPoint = Random.onUnitSphere * 45f; }
            //생성하려던 자리에 운석이나 공룡이 있다면 랜덤한 위치를 다시 구합니다.
            while (!Physics.OverlapSphere(respawnPoint, spwnRadius, layersToCheck).Length.Equals(0));

            // 생성하려는 자리에 공룡도, 운석도 없으므로 아이템을 생성합니다.
            GameObject item = itemList[Random.Range(0, itemList.Count)];
            //생성한 아이템은 대기열 리스트에서 제외합니다.
            itemList.Remove(item);

            //결정한 위치로 아이템을 이동시킵니다.
            item.transform.position = respawnPoint;
            item.transform.up = item.transform.position.normalized; //이 작업은, 아이템을 생성시점의 탄젠트 방향으로 바라보게 만드는 작업입니다.
            //필요한 준비가 끝났으므로 위치시킨 아이템을 활성화하여 보이게 만듭니다.
            item.gameObject.SetActive(true);
        }
    }
}

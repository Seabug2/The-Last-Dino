using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //* 아이템 스포너를 아이템 매니저로 새로 만들어주세요

    //* 아이템 생성 주기 float 변수
    [SerializeField] public float respawnTime = 10f;
    //아이템 리스트
    // 만들어주세요
    [SerializeField] public List<GameObject> itemList = new List<GameObject>();

    /// <summary>
    /// 아이템을 생성할 때 이 범위를 검사합니다.
    /// </summary>
    [SerializeField] public float spwnRadius = 10;

    private void Start()
    {
        //아이템을 먼저 비활성화 해둡니다.
        foreach(GameObject g in itemList)
        {
            g.SetActive(false);
        }

        //초기화 단계에서 게임매니저의 액션에 필요한 코루틴을 등록합니다.
        GameManager.instance.StartAction += () =>
        {
            StartCoroutine("AppearItem_co");
        };

        GameManager.instance.GameOverAction+= () =>
        {
            StopCoroutine("AppearItem_co");
            Destroy(this);
        };
    }


    private IEnumerator AppearItem_co()
    {
        int originCount = itemList.Count;

        // 검출할 다중 레이어를 만드는 방법
        int meteorLayer = LayerMask.NameToLayer("Meteor");
        int playerLayer = LayerMask.NameToLayer("Dino");
        //레이어 계산은 비트연산을 통해 이루어집니다.
        int combinedLayerMask = (1 << meteorLayer) | (1 << playerLayer);

        //랜덤한 위치를 저장할 변수를 캐싱해둡니다.
        Vector3 respawnPoint;

        while (true)
        {
            //respawnTime 시간 후에 아이템 생성을 할 수 있습니다.
            yield return new WaitForSeconds(respawnTime * Random.Range(.7f,1.2f));

            //아이템을 생성하려고 할 때 리스트 안에 아이템이 전부 모여있지 않으면 아이템 생성을 하지 않습니다.
            if (!itemList.Count.Equals(originCount)) continue;

            //랜덤한 위치를 구합니다.
            do { respawnPoint = Random.onUnitSphere * 45f; }
            //생성하려던 자리에 운석이나 공룡이 있다면 랜덤한 위치를 다시 구합니다.
            while (!Physics.OverlapSphere(respawnPoint, spwnRadius, combinedLayerMask).Equals(0));

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


    //* 캐릭터가 아이템을 획득하면 아이템은 아이템 매니저에게 자신의 종류를 알려주고 플레이어에게 특별한 효과를 주는 메서드를 실행하라고 알립니다.
    //* 플레이어에게 특별한 효과를 주는 메서드는 아이템 매니저가 가지고 있습니다.
    //* 자신에게 신호를 준 아이템의 종류를 파악하고 준비해뒀던 메서드 중에 알맞은 메서드를 실행합니다.
    //* 
    //* 특별한 효과는 코루틴으로, 실행되면 플레이어의 능력치를 바꾸거나 상태를 바꿉니다.
    //* 대기시간 (=지속시간)이 끝나면 원래 상태로 만들고 코루틴이 종료됩니다.
    //* 
    //* 아이템은 지구 표면의 무작위 위치에 생성됩니다.
    //* 이때 생성 범위를 검사해서 운석이 있거나 플레이어가 있으면 다른 무작위 위치를 구합니다.
    //* 아이템 한 번 생성을 하는데 while 문을 사용해야 할 겁니다.
    //* 
    //* 아이템이 생성되면 아이템 리스트에서 remove 됩니다.
    //* 온트리거 이벤트로 플레이어와 충돌하면 아이템 매니저에게 신호를 줍니다.
    //* 그리고 자신은 enable 합니다.
    //* 아이템은 returnlist 라는 컴포넌트를 가지고 있어서 비활성화 되면 아이템 매니저의 리스트에 add 됩니다.
}

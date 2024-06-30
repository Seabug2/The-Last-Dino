using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //* ������ �����ʸ� ������ �Ŵ����� ���� ������ּ���

    //* ������ ���� �ֱ� float ����
    [SerializeField] public float respawnTime = 10f;
    //������ ����Ʈ
    // ������ּ���
    [SerializeField] public List<GameObject> itemList = new List<GameObject>();

    /// <summary>
    /// �������� ������ �� �� ������ �˻��մϴ�.
    /// </summary>
    [SerializeField] public float spwnRadius = 10;

    private void Start()
    {
        //�������� ���� ��Ȱ��ȭ �صӴϴ�.
        foreach(GameObject g in itemList)
        {
            g.SetActive(false);
        }

        //�ʱ�ȭ �ܰ迡�� ���ӸŴ����� �׼ǿ� �ʿ��� �ڷ�ƾ�� ����մϴ�.
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

        // ������ ���� ���̾ ����� ���
        int meteorLayer = LayerMask.NameToLayer("Meteor");
        int playerLayer = LayerMask.NameToLayer("Dino");
        //���̾� ����� ��Ʈ������ ���� �̷�����ϴ�.
        int combinedLayerMask = (1 << meteorLayer) | (1 << playerLayer);

        //������ ��ġ�� ������ ������ ĳ���صӴϴ�.
        Vector3 respawnPoint;

        while (true)
        {
            //respawnTime �ð� �Ŀ� ������ ������ �� �� �ֽ��ϴ�.
            yield return new WaitForSeconds(respawnTime * Random.Range(.7f,1.2f));

            //�������� �����Ϸ��� �� �� ����Ʈ �ȿ� �������� ���� ������ ������ ������ ������ ���� �ʽ��ϴ�.
            if (!itemList.Count.Equals(originCount)) continue;

            //������ ��ġ�� ���մϴ�.
            do { respawnPoint = Random.onUnitSphere * 45f; }
            //�����Ϸ��� �ڸ��� ��̳� ������ �ִٸ� ������ ��ġ�� �ٽ� ���մϴ�.
            while (!Physics.OverlapSphere(respawnPoint, spwnRadius, combinedLayerMask).Equals(0));

            // �����Ϸ��� �ڸ��� ���浵, ��� �����Ƿ� �������� �����մϴ�.
            GameObject item = itemList[Random.Range(0, itemList.Count)];
            //������ �������� ��⿭ ����Ʈ���� �����մϴ�.
            itemList.Remove(item);

            //������ ��ġ�� �������� �̵���ŵ�ϴ�.
            item.transform.position = respawnPoint;
            item.transform.up = item.transform.position.normalized; //�� �۾���, �������� ���������� ź��Ʈ �������� �ٶ󺸰� ����� �۾��Դϴ�.
            //�ʿ��� �غ� �������Ƿ� ��ġ��Ų �������� Ȱ��ȭ�Ͽ� ���̰� ����ϴ�.
            item.gameObject.SetActive(true);
        }
    }


    //* ĳ���Ͱ� �������� ȹ���ϸ� �������� ������ �Ŵ������� �ڽ��� ������ �˷��ְ� �÷��̾�� Ư���� ȿ���� �ִ� �޼��带 �����϶�� �˸��ϴ�.
    //* �÷��̾�� Ư���� ȿ���� �ִ� �޼���� ������ �Ŵ����� ������ �ֽ��ϴ�.
    //* �ڽſ��� ��ȣ�� �� �������� ������ �ľ��ϰ� �غ��ص״� �޼��� �߿� �˸��� �޼��带 �����մϴ�.
    //* 
    //* Ư���� ȿ���� �ڷ�ƾ����, ����Ǹ� �÷��̾��� �ɷ�ġ�� �ٲٰų� ���¸� �ٲߴϴ�.
    //* ���ð� (=���ӽð�)�� ������ ���� ���·� ����� �ڷ�ƾ�� ����˴ϴ�.
    //* 
    //* �������� ���� ǥ���� ������ ��ġ�� �����˴ϴ�.
    //* �̶� ���� ������ �˻��ؼ� ��� �ְų� �÷��̾ ������ �ٸ� ������ ��ġ�� ���մϴ�.
    //* ������ �� �� ������ �ϴµ� while ���� ����ؾ� �� �̴ϴ�.
    //* 
    //* �������� �����Ǹ� ������ ����Ʈ���� remove �˴ϴ�.
    //* ��Ʈ���� �̺�Ʈ�� �÷��̾�� �浹�ϸ� ������ �Ŵ������� ��ȣ�� �ݴϴ�.
    //* �׸��� �ڽ��� enable �մϴ�.
    //* �������� returnlist ��� ������Ʈ�� ������ �־ ��Ȱ��ȭ �Ǹ� ������ �Ŵ����� ����Ʈ�� add �˴ϴ�.
}

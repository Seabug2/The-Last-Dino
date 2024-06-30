using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meat : MonoBehaviour
{
    // ��� �������� ȹ��� �̵��ӵ��� �ö� ���� ��¦�̴� ��ƼŬ�� �����
    [SerializeField] GameObject prtc; //��ƼŬ
    [SerializeField] GameObject Trex, Brachi, Archen;
    [SerializeField] float buffTime = 5f;
    public DinoData DinoData;
    private bool isBuffActive = false;
    
    // ���� ĳ���Ͷ��� �浹
    // �÷��̾�� �浹�ϸ�
    private void OnTriggerEnter(Collider other)
    {
        isBuffActive = true;
        prtc.SetActive(true);
        if(!isBuffActive)
        {
            StartCoroutine(EatMeat_CO());
        }
    }

    private IEnumerator EatMeat_CO()
    {
        ApplyBuff();

        yield return new WaitForSeconds(5f);

        RemoveBuff();
        isBuffActive = false;
        prtc.SetActive(false);
    }

    private void ApplyBuff()
    {
       if(gameObject.CompareTag("Trex"))
       {
           DinoData.speed += 5f;
       }
       if (gameObject.CompareTag("Brachi"))
       {
           DinoData.speed += 3f;
       }
       if (gameObject.CompareTag("Archen"))
       {
           DinoData.speed += 7f;
       }
    }
    private void RemoveBuff()
    {
        if (gameObject.CompareTag("Trex"))
        {
            DinoData.speed -= 5f;
        }
        if (gameObject.CompareTag("Brachi"))
        {
            DinoData.speed -= 3f;
        }
        if (gameObject.CompareTag("Archen"))
        {
            DinoData.speed -= 7f;
        }
    }

    // ��¦�̴� ����Ʈ�� ���

    // �ڷ�ƾ�� ����Ͽ� ������ �ð� ��ŭ �̵� �ӵ��� ������
    // �̵� �ӵ� ���� ���
    // �浹�� ��ü�� �θ� �ִ� movement�� ����
    // ������ �̵��ӵ��� ������ �ΰ�
    // movement�� �̵��ӵ��� ������
    // n ���� �ð��Ŀ� ������ �̵��ӵ��� �ǵ�����
    // �ڽ��� ��Ȱ��ȭ �ȴ�.
}

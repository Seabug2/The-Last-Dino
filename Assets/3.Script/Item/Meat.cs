using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meat : MonoBehaviour
{
    // 고기 아이템은 획득시 이동속도가 올라간 듯한 반짝이는 파티클이 실행됨
    [SerializeField] GameObject prtc; //파티클
    [SerializeField] GameObject Trex, Brachi, Archen;
    [SerializeField] float buffTime = 5f;
    public DinoData DinoData;
    private bool isBuffActive = false;
    
    // 공룡 캐릭터랑만 충돌
    // 플레이어와 충돌하면
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

    // 반짝이는 이펙트를 재생

    // 코루틴을 사용하여 정해진 시간 만큼 이동 속도를 높여줌
    // 이동 속도 접근 방법
    // 충돌한 객체의 부모에 있는 movement에 접근
    // 최초의 이동속도를 저장해 두고
    // movement의 이동속도를 높여줌
    // n 초의 시간후에 최초의 이동속도로 되돌리고
    // 자신은 비활성화 된다.
}

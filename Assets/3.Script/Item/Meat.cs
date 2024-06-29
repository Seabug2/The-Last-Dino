using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meat : MonoBehaviour
{
    // 고기 아이템은 획득시 이동속도가 올라간 듯한 반짝이는 파티클이 실행됨
    [SerializeField] GameObject prtc; //파티클
   
   
    // 공룡 캐릭터랑만 충돌
    // 플레이어와 충돌하면
    private void OnTriggerEnter(Collider other)
    {
        
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

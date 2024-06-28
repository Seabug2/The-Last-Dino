using UnityEngine;

[CreateAssetMenu]
public class DinoData : ScriptableObject
{
    //속도
    public float speed;

    //방향전환 속도
    public float angluerSpeed;

    //고기지속시간? 속도증가량
    public float meatTime;

    //방어 가능 횟수
    public float parasolTime;

    //체공 비행 지속 시간
    public float flightTime;

    public Vector3 localCapPosition;
    public Vector3 localCapScale;
    
    public Vector3 localParasolPosition;
    public Vector3 localParasolScale;
}
using UnityEngine;

[CreateAssetMenu]
public class DinoData : ScriptableObject
{
    //�ӵ�
    public float speed;

    //������ȯ �ӵ�
    public float angluerSpeed;

    //������ӽð�? �ӵ�������
    public float meatTime;

    //��� ���� Ƚ��
    public float parasolTime;

    //ü�� ���� ���� �ð�
    public float flightTime;

    public Vector3 localCapPosition;
    public Vector3 localCapScale;
    
    public Vector3 localParasolPosition;
    public Vector3 localParasolScale;
}
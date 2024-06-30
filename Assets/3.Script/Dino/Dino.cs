using System.Collections;
using UnityEngine;

public class Dino : MonoBehaviour
{
    public GameObject propeller;
    public GameObject umbrella;
    public ParticleSystem getPrtc;

    [HideInInspector]
    public Movement movement;

    float flightHeight = 12;
    float additionalSpeed = 5;

    private void Start()
    {
        propeller.SetActive(false);
        umbrella.SetActive(false);
    }

    public void GetItem(ItemType _type)
    {
        getPrtc.Play();
        switch (_type)
        {
            case ItemType.Meat:
                StartCoroutine(EatMeat());
                break;
            case ItemType.Propeller:
                StartCoroutine(WearHat());
                break;
            case ItemType.Umbrella:
                StartCoroutine(UseUmbrella_co());
                break;
        }
    }

    /// <summary>
    /// øÏªÍ æ∆¿Ã≈€ »πµÊ
    /// </summary>
    /// <returns></returns>
    public IEnumerator UseUmbrella_co()
    {
        umbrella.SetActive(true);

        gameObject.layer = 0;

        float time = movement.Data.parasolTime;
        yield return new WaitForSeconds(time);

        gameObject.layer = LayerMask.NameToLayer("Dino");
        umbrella.SetActive(false);
    }

    /// <summary>
    /// ∏¿⁄ æ∆¿Ã≈€ »πµÊ
    /// </summary>
    /// <returns></returns>
    public IEnumerator WearHat()
    {
        propeller.SetActive(true);

        Vector3 originLocalPosition = transform.localPosition;
        transform.localPosition += Vector3.up * flightHeight;

        float time = movement.Data.flightTime;
        yield return new WaitForSeconds(time);

        transform.localPosition = originLocalPosition;
        propeller.SetActive(false);
    }

    /// <summary>
    /// ∞Ì±‚ æ∆¿Ã≈€ »πµÊ
    /// </summary>
    /// <returns></returns>
    public IEnumerator EatMeat()
    {
        float defaultSpeed = movement.Data.speed;
        movement.speed += additionalSpeed;

        float time = movement.Data.flightTime;
        yield return new WaitForSeconds(time);

        movement.speed = defaultSpeed;
        propeller.SetActive(false);
    }
}
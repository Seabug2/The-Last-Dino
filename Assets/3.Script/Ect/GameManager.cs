using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (ReferenceEquals(instance, null))
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    Animator blackBoard;

    public void GameStart(Transform _selected)
    {   
        // 시네머신으로 가상 카메라를 바꾼다.
        Camera.main.GetComponent<CameraController>().ShiftVirtualCam();

        // 게임 시작시 선택된 공룡이 이동할 수 있게 부모 오브젝트에 Movement를 추가
        Movement movement = _selected.parent.gameObject.AddComponent<Movement>();
        // Resources 폴더로부터 객체 이름으로 Data 파일을 불러온다.
        DinoData dinoData = Resources.Load<DinoData>($"Dino Data/{_selected.name}");
        movement.SetData(dinoData);
    }

    public void GameOver(GameObject _dino)
    {
        Destroy(_dino);
        //파티클
        //결과창
    }

    public void Restart()
    {
        StartCoroutine(Restart_co());
    }

    IEnumerator Restart_co()
    {
        blackBoard.SetTrigger("Fade Out");
        
        yield return null;
        yield return new WaitWhile(()=> blackBoard.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);

        SceneManager.LoadScene(0);
        blackBoard.SetTrigger("Fade In");

        yield return null;
        yield return new WaitWhile(()=> blackBoard.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
        
        blackBoard.gameObject.SetActive(false);
    }
}

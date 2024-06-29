using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
    public enum State
    {
        Ready,
        InGame,
        GameOver
    }

public class GameManager : MonoBehaviour
{
    public State state = State.Ready;

    public static GameManager instance;

    private void Awake()
    {
        if (ReferenceEquals(instance, null))
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float score;

    public void GameStart(Transform _selected)
    {   
        // 시네머신으로 가상 카메라를 바꾼다.
        Camera.main.GetComponent<CameraController>().ShiftVirtualCam();
        // 게임 시작시 선택된 공룡이 이동할 수 있게 부모 오브젝트에 Movement를 추가
        Movement movement = _selected.parent.gameObject.AddComponent<Movement>();
        // Resources 폴더로부터 객체 이름으로 Data 파일을 불러온다.
        DinoData dinoData = Resources.Load<DinoData>($"Dino Data/{_selected.name}");
        movement.SetData(dinoData);
        state = State.InGame;
        StartAction?.Invoke();
    }

    // 운석이 호출
    public void GameOver(GameObject _dino)
    {
        state = State.GameOver;
        Camera.main.GetComponent<CameraController>().DisconnectTrace();
        Destroy(_dino.transform.gameObject);
        GameOverAction?.Invoke();
    }

    public event UnityAction StartAction;
    public event UnityAction GameOverAction;

    [SerializeField]
    Animator blackBoard;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator RestartScene_co()
    {
        yield return null;
    }
}

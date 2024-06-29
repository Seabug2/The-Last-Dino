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
        // �ó׸ӽ����� ���� ī�޶� �ٲ۴�.
        Camera.main.GetComponent<CameraController>().ShiftVirtualCam();
        // ���� ���۽� ���õ� ������ �̵��� �� �ְ� �θ� ������Ʈ�� Movement�� �߰�
        Movement movement = _selected.parent.gameObject.AddComponent<Movement>();
        // Resources �����κ��� ��ü �̸����� Data ������ �ҷ��´�.
        DinoData dinoData = Resources.Load<DinoData>($"Dino Data/{_selected.name}");
        movement.SetData(dinoData);
        state = State.InGame;
        StartAction?.Invoke();
    }

    // ��� ȣ��
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

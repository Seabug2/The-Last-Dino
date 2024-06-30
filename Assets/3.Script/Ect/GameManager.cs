using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
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
        if (instance == null)
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
    public void GameOver(Transform _dino)
    {
        state = State.GameOver;
        Camera.main.GetComponent<CameraController>().DisconnectTrace();
        Destroy(_dino.parent.gameObject);
        GameOverAction?.Invoke();
    }

    public event UnityAction StartAction;
    public event UnityAction GameOverAction;

    public void QuitGame()
    {
#if UNITY_EDITOR
            // If we are running in the editor, stop playing the scene
            EditorApplication.isPlaying = false;
#else
        // If we are running in a build, quit the application
        Application.Quit();
#endif
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }
}

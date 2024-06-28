using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public void GameStart(GameObject _selected)
    {
        Movement movement = _selected.transform.parent.gameObject.AddComponent<Movement>();
        Camera.main.GetComponent<CameraController>().ShiftVirtualCam();
        DinoData dinoData = Resources.Load<DinoData>($"Dino Data/{_selected.name}");
        movement.SetData(dinoData);
    }

    //싱글톤

    //게임 전반에 사용가능한 메서드 구현

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}

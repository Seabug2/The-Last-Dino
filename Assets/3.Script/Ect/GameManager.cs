using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    Animator blackBoard;

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

    private void Start()
    {

    }

    public void GameStart(GameObject _selected)
    {
        Movement movement = _selected.transform.parent.gameObject.AddComponent<Movement>();
        Camera.main.GetComponent<CameraController>().ShiftVirtualCam();
        DinoData dinoData = Resources.Load<DinoData>($"Dino Data/{_selected.name}");
        movement.SetData(dinoData);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}

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

    public void GameStart(GameObject _selected)
    {
        Movement movement = _selected.transform.parent.gameObject.AddComponent<Movement>();
        Camera.main.GetComponent<CameraController>().ShiftVirtualCam();
        DinoData dinoData = Resources.Load<DinoData>($"Dino Data/{_selected.name}");
        movement.SetData(dinoData);
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

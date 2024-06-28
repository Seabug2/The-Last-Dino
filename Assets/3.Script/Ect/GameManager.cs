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
        // �ó׸ӽ����� ���� ī�޶� �ٲ۴�.
        Camera.main.GetComponent<CameraController>().ShiftVirtualCam();

        // ���� ���۽� ���õ� ������ �̵��� �� �ְ� �θ� ������Ʈ�� Movement�� �߰�
        Movement movement = _selected.parent.gameObject.AddComponent<Movement>();
        // Resources �����κ��� ��ü �̸����� Data ������ �ҷ��´�.
        DinoData dinoData = Resources.Load<DinoData>($"Dino Data/{_selected.name}");
        movement.SetData(dinoData);
    }

    public void GameOver(GameObject _dino)
    {
        Destroy(_dino);
        //��ƼŬ
        //���â
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

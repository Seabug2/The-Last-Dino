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

    public void GameStart(int _playerSelNum)
    {

    }

    //싱글톤

    //게임 전반에 사용가능한 메서드 구현

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}

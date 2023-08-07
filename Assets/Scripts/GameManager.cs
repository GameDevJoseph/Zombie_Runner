using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] Transform _spawnPosition;
    [SerializeField] Canvas _wonCanvas;
    private static GameManager _instance;
    bool _hasCompletedLevel = false;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Game Manager is null");

            return _instance;
        }
    }


    private void Awake()
    {
        _instance = this;
    }


    public void ReplayScene()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public Transform RespawnPlayer()
    {
        return _spawnPosition;
    }

    public void EnableWinningCanvas()
    {
        _wonCanvas.enabled = true;
        _hasCompletedLevel = true;
    }


    public bool CheckForLevelCompletion()
    {
        return _hasCompletedLevel;
    }


}

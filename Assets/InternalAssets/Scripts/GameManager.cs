using UnityEngine;
using UnityEngine.SceneManagement;

interface IChangeGameState
{
    void RestartGame();
    void PauseGame();
    void ResumeGame();
    void QuitGame();
}

public class GameManager : MonoBehaviour, IChangeGameState
{
    private const string GameSceneName = "Game";
    private const string StartScreenName = "StartScreen";

    static bool gameRestarted = false;

    private Transform _guiTransform;
    private GameObject _startScreen;
    void Start()
    {
        _guiTransform = GameObject.Find("GUI").transform;

        foreach(Transform child in _guiTransform)
        {
            if (child.name == StartScreenName)
            {
                _startScreen = child.gameObject;
            }
        }

        if (gameRestarted)
        {
            Time.timeScale = 1;
            _startScreen.SetActive(false);
        }
        else
        {
            Application.targetFrameRate = 60;
            Time.timeScale = 0;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        gameRestarted = true;
        SceneManager.LoadScene(GameSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

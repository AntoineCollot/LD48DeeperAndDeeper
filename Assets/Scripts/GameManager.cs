using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isGameOver = false;
    public static bool hasGameStarted = false;
    public static bool isEndSpawned = false;
    public static bool isGameWon = false;
    public const float MAX_DEPTH = 150;
    [System.Serializable] public class GameOverEvent : UnityEvent<bool> { }
    public GameOverEvent onGameOver = new GameOverEvent();
    public UnityEvent onEndSpawned = new UnityEvent();
    public UnityEvent onGameWon = new UnityEvent();
    public UnityEvent onGameStart = new UnityEvent();
    public static float Depth
    {
        get
        {
            return EnvironmentScrolling.Instance.transform.position.y;
        }
    }
    public static float Difficulty
    {
        get
        {
            return Depth / MAX_DEPTH;
        }
    }

    public static GameManager Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;

        isGameOver = false;
        hasGameStarted = false;
        isEndSpawned = false;
        isGameWon = false;
    }

    public static void GameOver(bool bloodyDeath = false)
    {
        if (isGameOver || isEndSpawned)
            return;
        isGameOver = true;
        Instance.onGameOver.Invoke(bloodyDeath);
        Debug.Log("GameOver");
    }

    public void StartGame()
    {
        if (hasGameStarted)
            return;
        hasGameStarted = true;

        onGameStart.Invoke();
    }

    public void OnEndSpawned()
    {
        isEndSpawned = true;
        onEndSpawned.Invoke();
    }

    public static void Win()
    {
        if (isGameWon)
            return;
        isGameWon = true;
        Instance.onGameWon.Invoke();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}

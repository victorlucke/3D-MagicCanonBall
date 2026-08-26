using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameDificulty { easy, medium, hard }
    public enum GamePhase { phase1, phase2, phase3 }
    public enum GameStatus { Unpause, Pause, Win, Over }
    public static event Action<int> OnCountChanged;
    public static event Action OnGameOver;
    public int maxCount;
    public int count { get; private set; }
    private GameStatus currentGameStatus;
    private GameDificulty currentDificulty;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
            Destroy(gameObject);

        currentGameStatus = GameStatus.Pause;

        maxCount = GameObject.FindGameObjectsWithTag("Pickup").Length;
    }

    /// <summary>
    /// Add to the count value and call for the event that change UI
    /// </summary>
    public void AddCount()
    {
        count++;

        OnCountChanged?.Invoke(count);
    }

    public void GameOver()
    {
        ChangeGameStatus(GameStatus.Over);
    }

    /// <summary>
    /// Pause Game
    /// </summary>
    public void PauseGame()
    {
        ChangeGameStatus(GameStatus.Pause);
    }

    /// <summary>
    /// Unpause game
    /// </summary>
    public void UnpauseGame()
    {
        ChangeGameStatus(GameStatus.Unpause);
    }

    /// <summary>
    /// Change the current game timeScale based on GameStatus
    /// </summary>
    /// <param name="newGameStatus">Pause, Unpause</param>
    void ChangeGameStatus(GameStatus newGameStatus)
    {
        currentGameStatus = newGameStatus;

        switch (currentGameStatus)
        {
            case GameStatus.Pause:
                Time.timeScale = 0;
                break;
            case GameStatus.Unpause:
                Time.timeScale = 1;
                break;
            case GameStatus.Over:
                OnGameOver?.Invoke();
                break;
        }
    }

    public void ChangeDificulty(GameDificulty newDificulty)
    {
        currentDificulty = newDificulty;
        Debug.Log(currentDificulty);
    }

    public float DificultyMultiplayer()
    {
        Debug.Log("running");
        if (currentDificulty == GameDificulty.hard)
            return 2f;
        else if (currentDificulty == GameDificulty.medium)
            return 2f;
        else
            return 1;
    }
}

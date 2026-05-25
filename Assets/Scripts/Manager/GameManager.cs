using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum GameStatus { Unpause, Pause }
    private GameStatus currentGameStatus;

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
        }
    }
}

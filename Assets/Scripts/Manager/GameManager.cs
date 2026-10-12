using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameDificulty { easy, medium, hard }
    public enum GamePhase { phase1, phase2, phase3 }
    public enum GameStatus { Unpause, Pause, Win, Over, Menu }
    public static event Action<int> OnCountChanged;
    public static event Action OnGameOver;
    public int count { get; private set; }
    /// <summary>
    /// return amount of mana in the current magic bar
    /// </summary>
    [SerializeField][Range(0, 10)] public float manaAmount;
    public bool NoMana { get; set; }
    public GameStatus CurrentGameStatus { get { return _currentGameStatus; } }
    [SerializeField]private GameStatus _currentGameStatus;
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

        //MenuGame();

        //maxCount = GameObject.FindGameObjectsWithTag("Pickup").Length;
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

    public void MenuGame()
    {
        ChangeGameStatus(GameStatus.Menu);
    }

    /// <summary>
    /// Load game in specified Scene
    /// </summary>
    /// <param name="mySceneID"></param>
    public void RestartGame(int mySceneID)
    {
        ResetData();
        ChangeGameStatus(GameStatus.Menu);
        SceneManager.LoadScene(mySceneID);
    }

    /// <summary>
    /// Change the current game timeScale based on GameStatus
    /// </summary>
    /// <param name="newGameStatus">Pause, Unpause</param>
    void ChangeGameStatus(GameStatus newGameStatus)
    {
        _currentGameStatus = newGameStatus;

        switch (_currentGameStatus)
        {
            case GameStatus.Pause:
                Time.timeScale = 0;
                break;
            case GameStatus.Unpause:
                Time.timeScale = 1;
                break;
            case GameStatus.Menu:
                Time.timeScale = 0;
                break;
            case GameStatus.Over:
                OnGameOver?.Invoke();
                Debug.Log("Game Over");
                break;
        }
    }

    public void ChangeDificulty(GameDificulty newDificulty)
    {
        currentDificulty = newDificulty;
        //Debug.Log(currentDificulty);
    }

    public float DificultyMultiplayer()
    {
        //Debug.Log("running");
        if (currentDificulty == GameDificulty.hard)
            return 3;
        else if (currentDificulty == GameDificulty.medium)
            return 2;
        else
            return 1;
    }

    private void ResetData()
    {
        manaAmount = 0;
    }
}

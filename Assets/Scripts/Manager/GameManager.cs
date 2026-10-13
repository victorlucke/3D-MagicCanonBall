using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameDificulty { easy, medium, hard }
    public enum GamePhase { phase1, phase2, phase3, Boss, End }
    public enum GameStatus { Unpause, Pause, Win, Over, Menu, Cutscene }
    public static event Action OnGameOver;
    /// <summary>
    /// return amount of mana in the current magic bar
    /// </summary>
    [Range(0, 10)] public float manaAmount;
    public bool NoMana { get; set; }
    public GameStatus CurrentGameStatus { get { return _currentGameStatus; } }
    public GamePhase CurrentGamePhase { get { return _currentGamePhase; } set { _currentGamePhase = value; } }
    public bool IsRespawnEnabled { get { return _isRespawnEnabled; } set { _isRespawnEnabled = value; } }
    [SerializeField] private GameStatus _currentGameStatus;
    [SerializeField] private GamePhase _currentGamePhase;
    [SerializeField]private bool _isRespawnEnabled;
    private GameDificulty currentDificulty;

    //old stuf
    public static event Action<int> OnCountChanged;
    public int count { get; private set; }

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
        if (_currentGameStatus != GameStatus.Menu)
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

    public void Cutscene()
    {
        ChangeGameStatus(GameStatus.Cutscene);
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

    public void ChangeGamePhase(GamePhase myGamePhase)
    {
        _currentGamePhase = myGamePhase;

        switch (_currentGamePhase)
        {
            case GamePhase.Boss:
                Time.timeScale = 0;
                break;
        }
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

    private void ResetData()
    {
        manaAmount = 0;
        _currentGameStatus = GameStatus.Menu;
        _currentGamePhase = GamePhase.phase1;
        _isRespawnEnabled = false;
    }
}

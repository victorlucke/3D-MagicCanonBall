using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuObject;
    [SerializeField] private GameObject overMenuObject;
    [SerializeField] private GameObject winnerMenuObject;
    [SerializeField] private GameObject pauseMenuObject;

    [SerializeField] private InputAction pauseAction;

    void OnEnable() => GameManager.OnGameOver += GameOverMenu;

    void OnDisable() => GameManager.OnGameOver -= GameOverMenu;

    void Awake()
    {
        pauseAction = InputSystem.actions.FindAction("Pause");
    }

    void Start()
    {
        UIManager.Instance.AccessMenu(gameObject, mainMenuObject);
        GameManager.Instance.MenuGame();
    }

    void Update()
    {
        if (pauseAction.IsPressed())
            PauseMenu();

        if (GameManager.Instance.CurrentGameStatus == GameManager.GameStatus.Pause)
            if (!pauseMenuObject.activeSelf)
                PauseMenu();
    }

    public void MainMenu()
    {
        GameManager.Instance.RestartGame(0);
    }

    /// <summary>
    /// reload scene index
    /// </summary>
    public void RestartPhase()
    {
        GameManager.Instance.RestartGame(0);
    }

    /// <summary>
    /// Unpause the game
    /// </summary>
    public void LeaveMenu()
    {
        UIManager.Instance.DeactivateAllMenus(gameObject);
        GameManager.Instance.UnpauseGame();
    }

    /// <summary>
    /// 
    /// </summary>
    void GameOverMenu()
    {
        if (!GameManager.Instance.IsRespawnEnabled)
            UIManager.Instance.AccessMenu(gameObject, overMenuObject);
        GameManager.Instance.UnpauseGame();
    }

    void PauseMenu()
    {
        GameManager.Instance.PauseGame();
        if (GameManager.Instance.CurrentGameStatus == GameManager.GameStatus.Pause)
            UIManager.Instance.AccessMenu(gameObject, pauseMenuObject);
    }
}

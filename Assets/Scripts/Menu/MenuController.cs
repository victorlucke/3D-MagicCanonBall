using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject introMenu;
    public GameObject finalPhaseMenu;
    public enum MenuActivate { Lose, Win, Main, Pause, Settings };
    private GameObject mainMenuObject;
    private GameObject loserMenuObject;
    private GameObject winnerMenuObject;
    private GameObject finalPhaseMenuObject;

    void Awake()
    {
        mainMenuObject = GameObject.Find("MainMenu");
        loserMenuObject = GameObject.Find("LoserMenu");
        winnerMenuObject = GameObject.Find("WinnerMenu");
        finalPhaseMenuObject = GameObject.Find("FinalPhaseMenu");

    }


    void Start()
    {
        AccessMenu(MenuActivate.Main);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        GameManager.Instance.PauseGame();
    }

    public void StartGame()
    {
        DeactivateAllMenus();
        GameManager.Instance.UnpauseGame();
    }

    private void ShowMainMenu(bool isToShow)
    {
        mainMenuObject.SetActive(isToShow);
    }

    private void ShowLoserMenu(bool isToShow)
    {
        loserMenuObject.SetActive(isToShow);
    }

    private void ShowWinnerMenu(bool isToShow)
    {
        winnerMenuObject.SetActive(isToShow);
    }

    private void ShowPauseMenu(bool isToShow)
    {
        finalPhaseMenu.SetActive(isToShow);
    }

    private void DeactivateAllMenus()
    {
        int numberOfMenus;

        numberOfMenus = transform.childCount;

        for (int i = 0; i < numberOfMenus; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Access any menu, based on wich name you pass
    /// </summary>
    /// <param name="newMenu">MenuActivate.nameMenu</param>
    public void AccessMenu(MenuActivate newMenu)
    {

        GameManager.Instance.PauseGame();

        switch (newMenu)
        {
            case MenuActivate.Main:
                DeactivateAllMenus();
                ShowMainMenu(true);
                break;
            case MenuActivate.Lose:
                DeactivateAllMenus();
                ShowLoserMenu(true);
                break;
            case MenuActivate.Win:
                DeactivateAllMenus();
                ShowWinnerMenu(true);
                break;
            case MenuActivate.Pause:
                DeactivateAllMenus();
                ShowPauseMenu(true);
                break;
        }
    }
}

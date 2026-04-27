using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject introMenu;
    public GameObject finalPhaseMenu;

    void Start()
    {
        Time.timeScale = 0;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        introMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void BeginFinalPhase()
    {
        finalPhaseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}

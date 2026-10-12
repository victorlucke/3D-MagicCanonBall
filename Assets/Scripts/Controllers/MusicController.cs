using UnityEngine;

public class MusicController : MonoBehaviour
{
    void Update()
    {
        if (GameManager.Instance.CurrentGameStatus == GameManager.GameStatus.Menu)
            AudioManager.Instance.ChangeMusic(AudioManager.PlayMusic.Menu);

        if (GameManager.Instance.CurrentGameStatus == GameManager.GameStatus.Win)
             AudioManager.Instance.ChangeMusic(AudioManager.PlayMusic.Winner);

        if (GameManager.Instance.CurrentGameStatus == GameManager.GameStatus.Unpause)
             AudioManager.Instance.ChangeMusic(AudioManager.PlayMusic.Phase1);
    }
}

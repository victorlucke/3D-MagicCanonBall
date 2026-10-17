using UnityEngine;

public class DestroyOnWin : MonoBehaviour
{
    [SerializeField] private GameObject destroyThisObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.CurrentGameStatus == GameManager.GameStatus.Win)
        {
            Destroy(destroyThisObject);
        }
    }
}

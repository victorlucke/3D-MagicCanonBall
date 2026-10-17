using UnityEngine;

public class Rabbit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.CurrentGameStatus == GameManager.GameStatus.Win)
        {
            StartWave();
        }
    }

    void StartWave()
    {
        Animator myAnimator;

        myAnimator = GetComponent<Animator>();

        myAnimator.SetBool("IsCelebrating", true);
    }
}

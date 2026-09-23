using System.Runtime.CompilerServices;
using UnityEngine;

public class ChangeDificultyZone : MonoBehaviour
{
    public GameManager.GameDificulty zoneDificulty;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            GameManager.Instance.ChangeDificulty(zoneDificulty);
    }
}

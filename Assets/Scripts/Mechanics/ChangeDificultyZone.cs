using System.Runtime.CompilerServices;
using UnityEngine;

public class ChangeDificultyZone : MonoBehaviour
{
    public GameManager.GameDificulty zoneDificulty;

    void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.ChangeDificulty(zoneDificulty);
    }
}

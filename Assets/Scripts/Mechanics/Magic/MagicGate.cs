using UnityEngine;

public class MagicGate : MonoBehaviour
{
    void OnEnable()
    {
        GameEvents.OnOpenMagicGate += OpeningMagicGate;
    }

    void OnDisable()
    {
        GameEvents.OnOpenMagicGate -= OpeningMagicGate;
    }

    void OpeningMagicGate(GameObject currentGateToOpen)
    {
        if (currentGateToOpen == gameObject)
            Destroy(gameObject);

    }
}

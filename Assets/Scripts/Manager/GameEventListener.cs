using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public UnityEvent<GameObject> OnTargetObjectByLayer;
    public UnityEvent<GameObject> OnFireCannon;

    void OnEnable()
    {
        GameEvents.OnAimingLayer += ReactToAimingLayer;
        GameEvents.OnCannonFired += ReactToCannonFire;
    }

    void OnDisable()
    {
        GameEvents.OnAimingLayer -= ReactToAimingLayer;
        GameEvents.OnCannonFired -= ReactToCannonFire;
    }

    void ReactToAimingLayer(GameObject targtedObject)
    {
        OnTargetObjectByLayer?.Invoke(targtedObject);
    }

    void ReactToCannonFire(GameObject firedObject)
    {
        OnFireCannon?.Invoke(firedObject);
    }
}

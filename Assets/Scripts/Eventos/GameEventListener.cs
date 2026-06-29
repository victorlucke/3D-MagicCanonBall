using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public UnityEvent<GameObject> OnTargetObjectByLayer;
    public UnityEvent<GameObject> OnFireCannon;
    public UnityEvent<GameObject> OnBreakObject;

    void OnEnable()
    {
        GameEvents.OnAimingLayer += ReactToAimingLayer;
        GameEvents.OnCannonFired += ReactToCannonFire;
        GameEvents.OnBreakObject += ReactToBreakObject;
    }

    void OnDisable()
    {
        GameEvents.OnAimingLayer -= ReactToAimingLayer;
        GameEvents.OnCannonFired -= ReactToCannonFire;
        GameEvents.OnBreakObject -= ReactToBreakObject;
    }

    void ReactToAimingLayer(GameObject targtedObject)
    {
        OnTargetObjectByLayer?.Invoke(targtedObject);
    }

    void ReactToCannonFire(GameObject firedObject)
    {
        OnFireCannon?.Invoke(firedObject);
    }

    void ReactToBreakObject(GameObject brokenObject)
    {
        OnBreakObject?.Invoke(brokenObject);
    }
}

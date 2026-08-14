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

    /// <summary>
    /// this event is to be called inside the object you need to check if its on the aime of the cannon
    /// </summary>
    /// <param name="targtedObject"></param>
    void ReactToAimingLayer(GameObject targtedObject)
    {
        OnTargetObjectByLayer?.Invoke(targtedObject);
    }

    /// <summary>
    /// this event check if the canon has fired or not
    /// </summary>
    /// <param name="firedObject"></param>
    void ReactToCannonFire(GameObject firedObject)
    {
        OnFireCannon?.Invoke(firedObject);
    }

    /// <summary>
    /// no current use
    /// </summary>
    /// <param name="brokenObject"></param>
    void ReactToBreakObject(GameObject brokenObject)
    {
        OnBreakObject?.Invoke(brokenObject);
    }
}

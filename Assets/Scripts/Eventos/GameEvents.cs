using System;
using UnityEngine;

public static class GameEvents
{
    /// <summary>
    /// Save the ammo (gameObject) shooted by the cannon
    /// </summary>
    public static Action<GameObject> OnCannonFired;
    public static void TriggerOnCannonFired(GameObject projectile) => OnCannonFired?.Invoke(projectile);

    /// <summary>
    /// Is used to check if the object in front of a aime is of the corresponding layer 
    /// before realize aditional action
    /// </summary>
    public static Action<GameObject> OnAimingLayer;
    public static void TriggerOnAimingLayer(GameObject objectTargetLayer) => OnAimingLayer?.Invoke(objectTargetLayer);

    /// <summary>
    /// Is used to start the breaking process of an object breakable
    /// </summary>
    public static Action<GameObject> OnBreakObject;
    public static void TriggerOnBreakObject(GameObject objectBroken) => OnBreakObject?.Invoke(objectBroken);

    /// <summary>
    /// Is used to increase values of a slider bar
    /// </summary>
    public static Action<float> OnFillBar;
    public static void TriggerOnFillBar(float pickupValue) => OnFillBar?.Invoke(pickupValue);
}

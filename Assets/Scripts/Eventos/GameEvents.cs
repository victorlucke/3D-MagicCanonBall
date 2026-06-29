using System;
using UnityEngine;

public static class GameEvents
{
    /// <summary>
    /// Save the ammo shooted by the cannon
    /// </summary>
    public static Action<GameObject> OnCannonFired;
    public static void TriggerOnCannonFired(GameObject projectile) => OnCannonFired?.Invoke(projectile);

    public static Action<GameObject> OnAimingLayer;
    public static void TriggerOnAimingLayer(GameObject objectTargetLayer) => OnAimingLayer?.Invoke(objectTargetLayer);

    public static Action<GameObject> OnBreakObject;
    public static void TriggerOnBreakObject(GameObject objectBroken) => OnBreakObject?.Invoke(objectBroken);
}

using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<GameObject> OnCannonFired;
    public static void TriggerOnCannonFired(GameObject projectile) => OnCannonFired?.Invoke(projectile);

    public static event Action<GameObject> OnAimingLayer;
    public static void TriggerOnAimingLayer(GameObject objectTargetLayer) => OnAimingLayer?.Invoke(objectTargetLayer);

}

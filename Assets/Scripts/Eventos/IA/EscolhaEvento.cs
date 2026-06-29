using System;
using System.Reflection;
using UnityEngine;

[Serializable]
public class EscolhaEvento
{
    public string eventName;

    public void Invoke(GameObject obj)
    {
        FieldInfo field =
            typeof(GameEvents).GetField(
                eventName,
                BindingFlags.Public | BindingFlags.Static);

        if (field == null)
        {
            Debug.LogWarning($"Evento {eventName} não encontrado.");
            return;
        }

        Action<GameObject> action =
            field.GetValue(null) as Action<GameObject>;

        action?.Invoke(obj);
    }
}
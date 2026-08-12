using System;
using System.Reflection;
using UnityEngine;


/// <summary>
/// this class can be used as a public variable (EscolaEvento 'varVame') to serialize a dropdow ui interface
/// with every event present insede GameEvents, 
/// allowing you to select what event you want to use in the inspector.
/// </summary>
[Serializable]
public class EscolhaEvento
{
    public string eventName;

    /// <summary>
    /// invoke the event called by the external script inside GameEvents class with a GameObject as parameter
    /// </summary>
    /// <param name="obj"></param>
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
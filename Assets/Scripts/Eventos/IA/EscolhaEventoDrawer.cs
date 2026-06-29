using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Coleta todos os nome de eventos do tipo Public Static event Action<GameObject> 'Nome' e os cria como
/// opçoes serializadas no inspector para o usuario escolher em um drop down. 
/// Ele coleta os nomes na linha 23 dentro da variavel var eventInfo como eventInfo, para casos em que
/// o evento sao public static Action<GameObject> mudar de eventInfo para 
/// field = typeof('NomeClasseDeEventos').GetFields(...)
/// na linha 32 e 33, mudar os nomes do 
/// foreach(f in field) e
/// if(f.FieldType == ...)
/// </summary>
[CustomPropertyDrawer(typeof(EscolhaEvento))]
public class EscolhaEventoDrawer : PropertyDrawer
{
    string[] eventNames;

    public override void OnGUI(Rect position,
        SerializedProperty property,
        GUIContent label)
    {
        if (eventNames == null)
        {
            var eventInfo = typeof(GameEvents)
                .GetFields(BindingFlags.Public | BindingFlags.Static);

            List<string> names = new();

            foreach (var e in eventInfo)
            {
                if (e.FieldType == typeof(Action<GameObject>))
                    names.Add(e.Name);
            }

            eventNames = names.ToArray();
        }

        SerializedProperty eventName =
            property.FindPropertyRelative("eventName");

        int index = Array.IndexOf(eventNames,
                                  eventName.stringValue);

        if (index < 0)
            index = 0;

        //Debug.Log($"Quantidade de eventos encontrados: {eventNames.Length}");

        index = EditorGUI.Popup(position,
            label.text,
            index,
            eventNames);

        eventName.stringValue = eventNames[index];
    }
}

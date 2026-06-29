using System;
using UnityEngine;

public class BasicFunctionalities : MonoBehaviour
{
    [SerializeField] protected AudioClip audioEffect;

    protected void PlaySoundEffect()
    {
        AudioPlayer playSoundEffect = new AudioPlayer();

        if (audioEffect != null)
            playSoundEffect.PlaySound(gameObject, audioEffect);
        else
            Debug.Log("No AudioClip in " + gameObject.name);
    }
}

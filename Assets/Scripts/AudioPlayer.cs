using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioPlayer
{
    public EscolhaEvento evento;
    public List<AudioClip> audioOnEvent;
    public List<UnityEvent<GameObject>> eventWithAudio;

    public void PlaySound(GameObject myObject, AudioClip breakSound)
    {
        AudioSource myAudioSource;

        if (!myObject.GetComponent<AudioSource>())
        {
            myAudioSource = myObject.AddComponent<AudioSource>();
            myAudioSource.clip = breakSound;
            myAudioSource.Play();
        }
        else
        {
            myAudioSource = myObject.GetComponent<AudioSource>();
            myAudioSource.clip = breakSound;
            myAudioSource.Play();
        }
    }
}

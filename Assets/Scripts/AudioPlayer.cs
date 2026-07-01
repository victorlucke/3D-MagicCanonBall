using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioPlayer
{
    private AudioSource myAudioSource;

    /// <summary>
    /// Play a sound in the object that call
    /// </summary>
    /// <param name="myObject">the object source of the audio</param>
    /// <param name="newAudioClip">the audio to play</param>
    public void PlaySound(GameObject myObject, AudioClip newAudioClip)
    {
        CheckForAudioSource(myObject);

        if (!myAudioSource)
        {
            myAudioSource.clip = newAudioClip;
            myAudioSource.Play();
        }
    }

    public void StandardConfigAudioSource(AudioSource ConfiguredAudioSource)
    {
        ConfiguredAudioSource.spatialBlend = 0.65f;
    }

    /// <summary>
    /// Seach or create a new audio source in object if null. Save it on myAudioSource
    /// </summary>
    /// <param name="myObject">object to check for/create new audio source</param>
    public void CheckForAudioSource(GameObject myObject)
    {

        if (!myObject.GetComponent<AudioSource>())
        {
            myAudioSource = myObject.AddComponent<AudioSource>();
            StandardConfigAudioSource(myAudioSource);
        }
        else
        {
            myAudioSource = myObject.GetComponent<AudioSource>();
        }
    }
}

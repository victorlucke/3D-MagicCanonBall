using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public List<AudioClip> musicClips;
    public enum PlayMusic { Winner, Lose, Phase1, Phase2, Phase3 }
    private Dictionary<string, AudioClip> musicLoopDicionary = new Dictionary<string, AudioClip>();
    private AudioSource myAudioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        SaveAllMusics();

        myAudioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Collect all songs registred on the list audioClip and save them on a Dictionary for later use
    /// </summary>
    void SaveAllMusics()
    {
        foreach (AudioClip clip in musicClips)
        {
            if (!musicLoopDicionary.ContainsKey(clip.name))
            {
                musicLoopDicionary.Add(clip.name, clip);
            }
        }
    }

    /// <summary>
    /// Change the music to the current playMusic value when called
    /// </summary>
    public void ChangeMusic(PlayMusic newPlayMusic)
    {
        myAudioSource.Stop();

        switch (newPlayMusic)
        {
            case PlayMusic.Phase1:
                myAudioSource.clip = musicLoopDicionary["Phase1Music"];
                break;
            case PlayMusic.Winner:
                myAudioSource.clip = musicLoopDicionary["WinnerMusic"];
                break;
        }
    }

    /// <summary>
    /// Play a Music effect in the object that call
    /// </summary>
    /// <param name="myObject">the object source of the audio</param>
    /// <param name="newAudioClip">the audio to play</param>
    public void PlayClipEffect(GameObject myObject, AudioClip newAudioClip)
    {
        AudioSource otherAudioSource = CheckForAudioSource(myObject);

        if (otherAudioSource)
        {
            otherAudioSource.clip = newAudioClip;
            otherAudioSource.Play();
        }
    }

    /// <summary>
    /// Seach or create a new audio source in object if null. Save it on myAudioSource
    /// </summary>
    /// <param name="myObject">object to check for/create new audio source</param>
    AudioSource CheckForAudioSource(GameObject myObject)
    {
        AudioSource newAudioSource;

        if (!myObject.GetComponent<AudioSource>())
        {
            newAudioSource = myObject.AddComponent<AudioSource>();
            StandardConfigAudioSource(newAudioSource);

            return newAudioSource;
        }
        else
        {
            newAudioSource = myObject.GetComponent<AudioSource>();

            return newAudioSource;
        }
    }

    void StandardConfigAudioSource(AudioSource ConfiguredAudioSource)
    {
        ConfiguredAudioSource.spatialBlend = 0.65f;
    }
}

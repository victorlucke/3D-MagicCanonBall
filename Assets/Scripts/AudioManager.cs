using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public List<AudioClip> audioClip;
    private Dictionary<string, AudioClip> musicLoopDicionary = new Dictionary<string, AudioClip>();
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        SaveAllMusics();

        audioSource = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Play the current music saved on AudioSource
    /// </summary>
    void MusicOnPlay()
    {
        if(audioSource != null)
            audioSource.Play();
    }

    /// <summary>
    /// Change the current music for any added music on the audiclipList
    /// </summary>
    /// <param name="musicName">The exactly name of the clip added</param>
    public void ChangeMusic(string musicName)
    {
        audioSource.Stop();

        if (musicLoopDicionary.TryGetValue(musicName, out AudioClip music))
            audioSource.clip = music;
        
        MusicOnPlay();
    }

    /// <summary>
    /// Collect all songs registred on the list audioClip and save them on a Dictionary for later use
    /// </summary>
    void SaveAllMusics()
    {
        foreach (AudioClip clip in audioClip)
        {
            if (!musicLoopDicionary.ContainsKey(clip.name))
            {
                musicLoopDicionary.Add(clip.name, clip);
            }
        }
    }
}

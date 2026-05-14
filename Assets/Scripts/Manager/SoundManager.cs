using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<AudioClip> audioClip;
    private Dictionary<string, AudioClip> musicLoopDicionary = new Dictionary<string, AudioClip>();
    private enum PlaySound{Winner, Lose, Phase1, Phase2, Phase3}
    private PlaySound playSound;
    private AudioSource audioSource;

    void Awake()
    {
        SaveAllMusics();
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(musicLoopDicionary.ContainsKey("Phase1Music"))
            audioSource.clip = musicLoopDicionary["Phase1Music"];
    }

    // Update is called once per frame
    void Update()
    {
        MusicOnPlay();
    }

    void MusicOnPlay()
    {
        if(!audioSource.isPlaying)
            audioSource.Play();
    }

    void ChangeMusic()
    {
        audioSource.Stop();

        switch (playSound)
        {
            case PlaySound.Phase1:
            audioSource.clip = musicLoopDicionary["Phase1Music"];
            break;
            case PlaySound.Winner:
            audioSource.clip = musicLoopDicionary["WinnerMusic"];
            break;
        }
    }

    public void PlayWinMusic()
    {
        playSound = PlaySound.Winner;
        ChangeMusic();
    }

    void SaveAllMusics()
    {
        foreach (AudioClip clip in audioClip)
        {
            if (!musicLoopDicionary.ContainsKey(clip.name))
            {
                musicLoopDicionary.Add(clip.name, clip);
                Debug.Log("dicionario " + musicLoopDicionary[clip.name]);
            }
        }
    }
}

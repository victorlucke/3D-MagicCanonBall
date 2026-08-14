using System.Collections.Generic;
using UnityEngine;

public class BasicFunctionalities : MonoBehaviour
{
    [Header("Start BasicFunctionalities")]
    [Header("AudioConfigs")]
    [SerializeField] protected List<AudioClip> audioEffect;
    private bool isPlayingLoop;

    /// <summary>
    /// Play audio clip using audioManager, if more them one audio is saved, choose a random one.
    /// </summary>
    /// <param name="myAudioEffect">List of audios to be randomly selected</param>
    protected void PlaySoundEffect(List<AudioClip> myAudioEffect)
    {
        if (myAudioEffect.Count > 0)
        {
            int randomAudio = Random.Range(0, myAudioEffect.Count);

            AudioManager.Instance.PlayClipEffect(gameObject, myAudioEffect[randomAudio]);
        }
        else
            Debug.Log("No AudioClip in " + gameObject.name);
    }

    /// <summary>
    /// Play audio clip using audioManager.
    /// </summary>
    /// <param name="myAudioEffect">audio to play</param>
    protected void PlaySoundEffect(AudioClip myAudioEffect)
    {

        if (myAudioEffect)
        {
            AudioManager.Instance.PlayClipEffect(gameObject, myAudioEffect);
        }
        else
            Debug.Log("No AudioClip in " + gameObject.name);
    }

    /// <summary>
    /// Play audio clip in loop using audioManager, if more them one audio is saved, choose a random one.
    /// </summary>
    /// <param name="myAudioEffect">List of audios to be randomly selected</param>
    protected void PlayLoopSoundEffect(List<AudioClip> myAudioEffect)
    {
        int randomAudio = Random.Range(0, myAudioEffect.Count);

        if (myAudioEffect[randomAudio])
        {
            AudioManager.Instance.PlayClipEffect(gameObject, myAudioEffect[randomAudio], true);
        }
        else
            Debug.Log("No AudioClip in " + gameObject.name);
    }

    /// <summary>
    /// Play loop audio clip using audioManager.
    /// </summary>
    /// <param name="sourceObject">object with audiosource to play</param>
    /// <param name="myAudioEffect">clip to play</param>
    protected void PlayLoopSoundEffect(GameObject sourceObject, AudioClip myAudioEffect)
    {

        if (myAudioEffect)
        {
            AudioManager.Instance.PlayClipEffect(gameObject, myAudioEffect, true);
        }
        else
            Debug.Log("No AudioClip in " + gameObject.name);
    }

    protected void StopSoundEffect(AudioSource myAudioSource)
    {
        AudioManager.Instance.StopClipEffect(gameObject);
    }
}

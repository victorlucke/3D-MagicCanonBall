using System.Collections.Generic;
using UnityEngine;

public class BasicFunctionalities : MonoBehaviour
{
    [Header("Start BasicFunctionalities")]
    [Header("AudioConfigs")]
    [SerializeField] protected List<AudioClip> audioEffect;

    /// <summary>
    /// Play audio clip using audioManager, if more them one audio is saved, choose a random one.
    /// </summary>
    protected void PlaySoundEffect()
    {
        int randomAudio = Random.Range(0, audioEffect.Count);

        if (audioEffect[randomAudio])
        {
            AudioManager.Instance.PlayClipEffect(gameObject, audioEffect[randomAudio]);
        }
        else
            Debug.Log("No AudioClip in " + gameObject.name);
    }

    /// <summary>
    /// Play audio clip in loop using audioManager, if more them one audio is saved, choose a random one.
    /// </summary>
    protected void PlayLoopSoundEffect()
    {
        int randomAudio = Random.Range(0, audioEffect.Count);

        if (audioEffect[randomAudio])
        {
            AudioManager.Instance.PlayClipEffect(gameObject, audioEffect[randomAudio], true);
        }
        else
            Debug.Log("No AudioClip in " + gameObject.name);
    }
}

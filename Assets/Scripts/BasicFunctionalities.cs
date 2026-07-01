using System.Collections.Generic;
using UnityEngine;

public class BasicFunctionalities : MonoBehaviour
{
    [Header("AudioCongigs")]
    [SerializeField] protected List<AudioClip> audioEffect;

    /// <summary>
    /// Play audio clip using abstract class AudioPlayer
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
}

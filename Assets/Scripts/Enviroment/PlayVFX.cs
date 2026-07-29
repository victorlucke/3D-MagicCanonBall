using UnityEngine;
using UnityEngine.VFX;

public class PlayVFX : MonoBehaviour
{
    public ParticleSystem ParticleSystem;
    void Awake()
    {
        if (!ParticleSystem)
            ParticleSystem = GetComponentInChildren<ParticleSystem>();
    }
    void OnTriggerEnter(Collider other)
    {
        ParticleSystem.Play();
    }
}

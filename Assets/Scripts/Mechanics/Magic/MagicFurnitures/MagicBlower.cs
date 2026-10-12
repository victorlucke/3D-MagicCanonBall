using System.Collections;
using UnityEditor.U2D.Sprites;
using UnityEngine;

public class MagicBlower : MagicAreaEffectFurniture
{
    [Header("Blower Class")]
    [SerializeField] private float blowSpeed;
    [SerializeField] private GameObject blowEffectObject;
    [SerializeField] private GameObject furnaceEffectObject;
    float sizeY;
    float sizeX;
    float sizeZ;

    protected override void Awake()
    {
        base.Awake();
        sizeY = transform.localScale.y;
        sizeX = transform.localScale.x;
        sizeZ = transform.localScale.z;
    }

    protected override IEnumerator ActivateAfterTime(float activationDelay)
    {
        yield return base.ActivateAfterTime(activationDelay);

        yield return StartCoroutine(Blow());
    }

    IEnumerator Blow()
    {
        float currentSizeY = sizeY;
        float minSizeY = .5f;
        float maxSizeY = 1f;
        bool isDone = false;
        Debug.Log("Start Coroutine Blow");

        VFXStart();

        while (!isDone)
        {
            Debug.Log("Loop Blow");
            while (currentSizeY > minSizeY)
            {
                currentSizeY -= blowSpeed * Time.deltaTime;
                transform.localScale = new Vector3(sizeX, currentSizeY, sizeZ);
                yield return null;
            }

            while (currentSizeY < maxSizeY)
            {
                currentSizeY += blowSpeed * Time.deltaTime;
                transform.localScale = new Vector3(sizeX, currentSizeY, sizeZ);
                yield return null;
            }

            transform.localScale = new Vector3(sizeX, sizeY, sizeZ);

            isDone = true;
        }

        isActivated = false;
    }

    /// <summary>
    /// Start particle effects
    /// </summary>
    private void VFXStart()
    {
        ParticleSystem blowParticle;
        ParticleSystem furnaceParticle;

        blowParticle = blowEffectObject.GetComponent<ParticleSystem>();
        furnaceParticle = furnaceEffectObject.GetComponent<ParticleSystem>();

        ConfigParticles(blowParticle);
        ConfigParticles(furnaceParticle);

        blowParticle.Play();
        furnaceParticle.Play();
    }

    private void ConfigParticles(ParticleSystem myParticle)
    {
        var MainModule = myParticle.main;
        float duration = FisicalEffectsManager.Instance.effectStandardDuration;

        if (MainModule.duration < duration)
        {
            myParticle.Stop();
            MainModule.duration = duration;
        }
    }
}

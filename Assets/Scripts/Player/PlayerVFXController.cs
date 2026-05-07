using UnityEngine;

public class PlayerVFXController : MonoBehaviour
{
    public GameObject dustObject;
    private PlayerController playerController;
    private Rigidbody playerRigidbody;
    private bool isPlayingVFX;

    void Awake()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void LateUpdate()
    {
        MovingDustVFX(1);
    }

    /// <summary>
    /// play the dust visual effect when player start moving
    /// </summary>
    /// <param name="minSpeedToDust">1 valor recomendado</param>
    void MovingDustVFX(float minSpeedToDust)
    {
        Vector3 dustFinalPosition = gameObject.transform.position;
        float playerCurrentSpeed = playerRigidbody.linearVelocity.magnitude;
        bool initialLoop = false;
        int initialCycle = 15;

        dustObject.transform.position = dustFinalPosition;

        SetDustModulesVFX(initialLoop, initialCycle);

        if (playerCurrentSpeed > minSpeedToDust && playerCurrentSpeed != 0)
        {
            if (!isPlayingVFX)
            {
                dustObject.transform.LookAt(playerController.oppositeDirection);
                dustObject.GetComponent<ParticleSystem>().Play();
                isPlayingVFX = true;
            }
        }
        else
            isPlayingVFX = false;
    }

    /// <summary>
    /// Rever isso para ver se esta certo **********************
    /// </summary>
    /// <param name="minSpeedToDust"></param>
    /// <param name="loop"></param>
    /// <param name="cycles"></param>
    void MovingDustVFX(float minSpeedToDust, bool newLoop, int newCycles)
    {
        Vector3 dustFinalPosition = gameObject.transform.position;
        float playerCurrentSpeed = playerRigidbody.linearVelocity.magnitude;

        dustObject.transform.position = dustFinalPosition;

        SetDustModulesVFX(newLoop, newCycles);

        if (playerCurrentSpeed > 1 && playerCurrentSpeed != 0)
        {
            if (!isPlayingVFX)
            {
                dustObject.transform.LookAt(playerController.oppositeDirection);
                dustObject.GetComponent<ParticleSystem>().Play();
                isPlayingVFX = true;
            }
        }
        else
            isPlayingVFX = false;
    }

    void SetDustModulesVFX(bool looping, int cycles)
    {
        ParticleSystem dustParticleSystem = dustObject.GetComponent<ParticleSystem>();

        ParticleSystem.MainModule dustMain = dustParticleSystem.main;
        ParticleSystem.Burst dustBurst = dustParticleSystem.emission.GetBurst(0);

        dustMain.loop = looping;
        dustBurst.cycleCount = cycles;
    }
}

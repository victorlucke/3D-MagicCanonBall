using UnityEngine;

public class PlayerVFXController : MonoBehaviour
{
    
    public GameObject dustObject;
    public GameObject skidDustObject;
    public float minSpeedToDust;
    public float maxSpeedToDust;
    private PlayerController playerController;
    private Rigidbody playerRigidbody;
    private bool isPlayingVFX;
    private float playerCurrentSpeed;
    private Vector3 pastVelocity;

    void Awake()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        playerRigidbody = gameObject.GetComponent<Rigidbody>();

        pastVelocity = playerRigidbody.linearVelocity;
    }

    void LateUpdate()
    {
        PlayDustOnMove();
    }

    /// <summary>
    /// Start animation on move, based on current speed of player
    /// </summary>
    void PlayDustOnMove()
    {
        playerCurrentSpeed = playerRigidbody.linearVelocity.magnitude;
        ParticleSystem dustParticleSystem = dustObject.GetComponent<ParticleSystem>();
        ParticleSystem skidDustParticleSystem = skidDustObject.GetComponent<ParticleSystem>();

        if (playerController.onGround)
        {

            if (playerCurrentSpeed > minSpeedToDust && playerCurrentSpeed < minSpeedToDust + 0.5)
                MovingDustVFX(dustParticleSystem);

            else if (playerCurrentSpeed > maxSpeedToDust && !IsSlowingDown())
            {
                MovingDustVFX(dustParticleSystem, true);
            }
            else if (IsSlowingDown() && playerCurrentSpeed > minSpeedToDust)
            {
                //Debug.Log(playerCurrentSpeed);
                SkidDustVFX();
            }
            else
            {
                isPlayingVFX = false;
                dustParticleSystem.Stop();
                skidDustParticleSystem.Stop();
            }
        }
        else
        {
            isPlayingVFX = false;
            dustParticleSystem.Stop();
            skidDustParticleSystem.Stop();
        }

        playerCurrentSpeed = 0;
    }

    /// <summary>
    /// verify if aceleration is increasing or decreasing
    /// </summary>
    /// <returns>true if is false if isnt</returns>
    private bool IsSlowingDown()
    {
        bool slowingDown;

        Vector3 playerAcceleration = (playerRigidbody.linearVelocity - pastVelocity) / Time.fixedDeltaTime;
        pastVelocity = playerRigidbody.linearVelocity;

        float accelerationDot = Vector3.Dot(playerRigidbody.linearVelocity, playerAcceleration);

        if (accelerationDot < 0)
        {
            slowingDown = true;
            return slowingDown;
        }
        else
        {
            slowingDown = false;
            return slowingDown;
        }
    }

    /// <summary>
    /// Position and iniciate the visual effect Skid
    /// </summary>
    void SkidDustVFX()
    {
        Vector3 dustFinalPosition = gameObject.transform.position;

        skidDustObject.transform.position = dustFinalPosition;

        if (playerController.playerMove)
        {
            skidDustObject.transform.LookAt(playerController.oppositeDirection);

            skidDustObject.GetComponent<ParticleSystem>().Stop();
            skidDustObject.GetComponent<ParticleSystem>().Play();
        }
    }

    /// <summary>
    /// Position and start the dust visual effect once
    /// </summary>
    /// <param name="dustPS"></param>
    void MovingDustVFX(ParticleSystem dustPS)
    {
        Vector3 dustFinalPosition = gameObject.transform.position;
        bool initialLoop = false;
        int initialCycle = 10;
        dustObject.transform.position = dustFinalPosition;

        if (!isPlayingVFX)
        {

            SetDustModulesVFX(initialLoop, initialCycle);

            dustObject.transform.LookAt(playerController.oppositeDirection);

            dustPS.Play();

            isPlayingVFX = true;
        }
    }

    /// <summary>
    /// Position and start the dust visual effect in loop
    /// </summary>
    /// <param name="dustPS"></param>
    /// <param name="startLoop">true value recommended</param>
    void MovingDustVFX(ParticleSystem dustPS, bool startLoop)
    {
        Vector3 dustFinalPosition = gameObject.transform.position;
        int newCycle = 0;

        dustObject.transform.position = dustFinalPosition;

        dustObject.transform.LookAt(playerController.oppositeDirection);

        if (!isPlayingVFX)
        {
            SetDustModulesVFX(startLoop, newCycle);

            dustPS.Play();

            isPlayingVFX = true;
        }
    }

    /// <summary>
    /// change parameters of particle to loop and cycle continuosly
    /// </summary>
    /// <param name="looping"></param>
    /// <param name="cycles"></param>
    void SetDustModulesVFX(bool looping, int cycles)
    {
        ParticleSystem dustParticleSystem = dustObject.GetComponent<ParticleSystem>();

        ParticleSystem.MainModule dustMain = dustParticleSystem.main;
        ParticleSystem.EmissionModule dustEmission = dustParticleSystem.emission;
        ParticleSystem.Burst dustBurst = dustEmission.GetBurst(0);

        dustMain.loop = looping;
        dustBurst.cycleCount = cycles;
        dustEmission.SetBurst(0, dustBurst);
    }
}

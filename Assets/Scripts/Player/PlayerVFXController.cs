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
    void PlayDustOnMove()
    {
        playerCurrentSpeed = playerRigidbody.linearVelocity.magnitude;
        ParticleSystem dustParticleSystem = dustObject.GetComponent<ParticleSystem>();
        ParticleSystem skidDustParticleSystem = skidDustObject.GetComponent<ParticleSystem>();

        if (playerController.onGround)
        {

            if (playerCurrentSpeed > minSpeedToDust && playerCurrentSpeed < minSpeedToDust + 0.5)
                MovingDustVFX(dustParticleSystem, minSpeedToDust);

            else if (playerCurrentSpeed > maxSpeedToDust && !IsSlowingDown())
            {
                MovingDustVFX(dustParticleSystem, maxSpeedToDust, true);
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

        playerCurrentSpeed = 0;
    }

    /// <summary>
    /// play the dust visual effect when player start moving
    /// </summary>
    /// <param name="minSpeed">min speed in magnitude to play the particles  (1 is the lower)</param>
    void MovingDustVFX(ParticleSystem dustPS, float minSpeed)
    {
        Vector3 dustFinalPosition = gameObject.transform.position;
        bool initialLoop = false;
        int initialCycle = 10;
        dustObject.transform.position = dustFinalPosition;


        // if (playerCurrentSpeed > minSpeed && playerCurrentSpeed < minSpeed + 0.5)
        // {
        if (!isPlayingVFX)
        {
            //Debug.Log("Stop Loop");

            SetDustModulesVFX(initialLoop, initialCycle);

            dustObject.transform.LookAt(playerController.oppositeDirection);

            dustPS.Play();

            isPlayingVFX = true;
        }
        // }
        // else if (playerCurrentSpeed < minSpeed)
        //     isPlayingVFX = false;
    }


    void MovingDustVFX(ParticleSystem dustPS, float maxSpeed, bool startLoop)
    {
        Vector3 dustFinalPosition = gameObject.transform.position;
        bool newLoop = true;
        int newCycle = 0;

        dustObject.transform.position = dustFinalPosition;

        // if (playerCurrentSpeed > maxSpeed)
        // {
        dustObject.transform.LookAt(playerController.oppositeDirection);

        if (!isPlayingVFX)
        {
            //Debug.Log("Start Loop");

            SetDustModulesVFX(newLoop, newCycle);

            dustPS.Play();

            isPlayingVFX = true;
        }
        // }
        // else
        //     isPlayingVFX = false;
    }

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

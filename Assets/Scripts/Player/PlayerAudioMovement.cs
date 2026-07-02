using UnityEngine;

//ROLING SOUND TERRAIN BY CHANGING PITCH
//sand .7 pitch
//Wood .6 pitch
//Stone .5 pitch
//Metal .4 pitch
public class PlayerAudioMovement : BasicFunctionalities
{
    public PlayerController playerController;
    public Rigidbody playerRigidbody;
    private float minSpeed;
    private float maxSpeed;
    private bool played;
    private AudioClip audioBallFall;
    private AudioClip audioBallRoll;
    private AudioClip audioBallRolling;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerRigidbody = GetComponent<Rigidbody>();
        audioBallRolling = audioEffect[0];
        minSpeed = 1;
        maxSpeed = 6;
    }

    void FixedUpdate()
    {
        CheckMovementAudio();
        ChangeAudioVolumePerSpeed();
    }

    void CheckMovementAudio()
    {
        float playerSpeed = playerRigidbody.linearVelocity.magnitude;
        bool isOnGround = playerController.onGround;

        if (!played)
        { 
            if (playerSpeed >= minSpeed && isOnGround)
            {
                PlayLoopSoundEffect(audioBallRolling);
                played = true;
            }
        }
        else if (played)
        {
            if (playerSpeed <= minSpeed || !isOnGround)
            {
                AudioSource thisAudioSource = GetComponent<AudioSource>();
                thisAudioSource.volume = 0;
                StopSoundEffect();
                played = false;
            }
        }
    }

    void ChangeAudioVolumePerSpeed()
    {
        float playerSpeed = playerRigidbody.linearVelocity.magnitude;

        if(playerSpeed < maxSpeed)
        {
            float distanceToMaxSpeed = playerSpeed / maxSpeed;
            AudioSource thisAudioSource = GetComponent<AudioSource>();
            thisAudioSource.volume = distanceToMaxSpeed;
            Debug.Log(distanceToMaxSpeed);
        }
    }

    void CheckPlayerInAir()
    {
        //if(isongro)
    }

    void OnCollisionEnter(Collision collision)
    {
        
    }
}

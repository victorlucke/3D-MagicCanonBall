using System;
using Unity.VisualScripting;
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
    private AudioSource audioSource;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioBallRolling = audioEffect[0];
        audioBallFall = audioEffect[1];
        minSpeed = 0.09f;
        maxSpeed = 6;
    }

    void FixedUpdate()
    {
        CheckMovementAudio();
    }

    void CheckMovementAudio()
    {
        float playerSpeed = playerRigidbody.linearVelocity.magnitude;
        bool isOnGround = playerController.onGround;

        ChangeAudioVolumePerSpeed(audioSource);

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
                ChangeAudioVolumePerSpeed(audioSource);
                played = false;
            }
        }
    }

    void ChangeAudioVolumePerSpeed(AudioSource thisAudioSource)
    {
        float playerSpeed = playerRigidbody.linearVelocity.magnitude;
        bool isOnGround = playerController.onGround;

        if (playerSpeed < maxSpeed && isOnGround)
        {
            if (playerSpeed >= minSpeed)
            {
                float distanceToMaxSpeed = playerSpeed / maxSpeed;
                thisAudioSource.volume = distanceToMaxSpeed;
            }
        }
        else if (playerSpeed <= minSpeed || !isOnGround)
        {
            thisAudioSource.volume = 0;
        }
    }

    void PlayNewClip(AudioClip myClip)
    {
        AudioSource newAudioSource;

        CreateAudioObject(myClip.name);

        newAudioSource = transform.Find(myClip.name).GetComponent<AudioSource>();
        newAudioSource.clip = myClip;
        newAudioSource.Play();
    }

    void CreateAudioObject(String newName)
    {
        if (!transform.Find(newName))
        {
            new GameObject(newName).transform.SetParent(gameObject.transform);
            transform.Find(newName).AddComponent<AudioSource>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            PlayNewClip(audioBallFall);
        }
    }
}

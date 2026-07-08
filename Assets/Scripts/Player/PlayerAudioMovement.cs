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
    public AudioSource audioSource;
    private float minSpeed;
    private float maxSpeed;
    private bool played;
    private string collisionLayer;
    private AudioClip audioBallFall;
    private AudioClip audioBallRoll;
    private AudioClip audioBallRolling;
    private float pitchValue;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioBallRolling = audioEffect[0];
        audioBallFall = audioEffect[1];
        Debug.Log("Awake audio Fall:  "+audioBallFall.name);
        minSpeed = 0.09f;
        maxSpeed = 6;
    }

    void FixedUpdate()
    {
        CheckMovementAudio(audioSource);
    }

    /// <summary>
    /// Check if ball player is moving, them play the loop for rolling
    /// </summary>
    /// <param name="loopMovementAudioSource">audio source to play the loop clip</param>
    void CheckMovementAudio(AudioSource loopMovementAudioSource)
    {
        float playerSpeed = playerRigidbody.linearVelocity.magnitude;
        bool isOnGround = playerController.onGround;

        ChangeAudioVolumePerSpeed(loopMovementAudioSource);
        ChangeAudioPitch(loopMovementAudioSource);

        if (!played)
        {
            if (playerSpeed >= minSpeed && isOnGround)
            {
                PlayLoopSoundEffect(loopMovementAudioSource.gameObject, audioBallRolling);

                played = true;
            }
        }
        else if (played)
        {
            if (playerSpeed <= minSpeed || !isOnGround)
            {
                ChangeAudioVolumePerSpeed(loopMovementAudioSource);
                played = false;
            }
        }
    }

    /// <summary>
    /// Change volume of audiosource based on speed for rolling ball experience
    /// </summary>
    /// <param name="thisAudioSource">audio source responsible for rolling audio</param>
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

    /// <summary>
    /// change the pitch value of audio to simulate diferent material you moving in
    /// </summary>
    /// <param name="thisAudioSource">audio source you want to apply the changes</param>
    void ChangeAudioPitch(AudioSource thisAudioSource)
    {
        thisAudioSource.pitch = pitchValue;
    }

    /// <summary>
    /// play a clip using a new audio source inside a parent object.
    /// </summary>
    /// <param name="myClip">clip to play</param>
    void PlayNewClip(AudioClip myClip)
    {
        AudioSource newAudioSource;

        CreateAudioObject(myClip.name);

        newAudioSource = transform.Find(myClip.name).GetComponent<AudioSource>();

        ChangeAudioPitch(newAudioSource);
        
        newAudioSource.PlayOneShot(myClip);
    }

    /// <summary>
    /// Create a new game object with audio source attached to it.
    /// </summary>
    /// <param name="newName">name of the new object</param>
    void CreateAudioObject(string newName)
    {
        if (!transform.Find(newName))
        {
            new GameObject(newName).transform.SetParent(gameObject.transform);
            transform.Find(newName).AddComponent<AudioSource>();
        }
    }

    /// <summary>
    /// Verify the layers in search of "terrain materials" to create ilusion of diferent sounds changing pitch value of audio source.
    /// </summary>
    /// <param name="layerName">the layer name you are checking</param>
    void VerifyLayer(string layerName)
    {
        switch (layerName)
        {
            case "Wood":
                pitchValue = .6f;
                break;
            case "Stone":
                pitchValue = .5f;
                break;
            case "Sand":
                pitchValue = .7f;
                break;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        collisionLayer = LayerMask.LayerToName(collision.gameObject.layer);
        VerifyLayer(collisionLayer);

        if (collision.gameObject.CompareTag("Ground"))
        {
            PlayNewClip(audioBallFall);
            Debug.Log("Collision audio Fall:  "+audioBallFall.name);
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public List<Transform> cutscenesTransform;
    [SerializeField] private bool isCutsceneGoing;
    [SerializeField] private bool isCutsceneOver;
    private Vector3 offset;
    private Vector3 offsetZoom;
    private Vector3 rotationInGame;
    private InputAction changeCameraAction;

    private bool isToChange;
    private bool isChangingPosition;

    void Awake()
    {
        offset = new Vector3(0, 10, -10);
        offsetZoom = new Vector3(3, 0, -3);
        rotationInGame = new Vector3(45, 0, 0);
        changeCameraAction = InputSystem.actions.FindAction("Interact");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindObject("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (GameManager.Instance.CurrentGamePhase == GameManager.GamePhase.Boss)
        {
            if (!isCutsceneOver)
                LocateBoss();

            if (isCutsceneOver)
            {
                LocatePlayer();
            }
        } 
        else if(GameManager.Instance.CurrentGamePhase == GameManager.GamePhase.End)
            LocateEndTarget();
            
        else LocatePlayer();
    }

    /// <summary>
    /// Detect object with Player tag save it and move towards him.
    /// </summary>
    void LocatePlayer()
    {
        if (player != null)
        {
            PositionCamera();
            // if (changeCameraAction.IsPressed() && !isToChange)
            // {
            //     isToChange = true;
            // }
            // else if (isToChange)
            //     isToChange = MoveCamera(cutscenesTransform[0], true);
        }
        else
            player = FindObject("Player");
    }

    /// <summary>
    /// 
    /// </summary>
    void LocateEndTarget()
    {
        GameObject endGameObject;

        endGameObject = FindObject("EndGame");

        if(endGameObject != null)
        {
            bool isThisCamMoving;
            isThisCamMoving = MoveCameraToward(endGameObject.transform, false);

            if(!isThisCamMoving) GameManager.Instance.StatusGameWin();
        }
    }

    /// <summary>
    /// Locate object with Boss tag and move towards it.
    /// </summary>
    void LocateBoss()
    {
        GameObject bossObject;

        GameManager.Instance.StatusCutscene();

        bossObject = FindObject("Boss");

        if (bossObject != null)
        {
            isCutsceneGoing = MoveCameraToward(bossObject.transform, true);

            if (!isCutsceneGoing)
            {
                isCutsceneOver = true;
                GameManager.Instance.StatusUnpauseGame();
            }
            else isCutsceneOver = false;
        }
        else
        {
            Debug.Log("Cant find boss");
            isCutsceneGoing = false;
            isCutsceneOver = true;
        }
    }

    GameObject FindObject(String myTagToSeach)
    {
        GameObject myObject;

        myObject = null;

        if (GameObject.FindWithTag(myTagToSeach) != null)
            myObject = GameObject.FindWithTag(myTagToSeach).gameObject;

        return myObject;
    }

    void PositionCamera()
    {
        transform.position = player.transform.position + offset;
        transform.rotation = Quaternion.Euler(rotationInGame);
    }

    /// <summary>
    /// Move the camera to a focus on a new transform.
    /// </summary>
    /// <param name="newTransforms"></param>
    /// <param name="rotate">is also to rotate to face the object (else use gameplay rotation)</param>
    /// <returns></returns>
    bool MoveCameraToward(Transform newTransforms, bool rotate)
    {
        bool isMoving = true;
        float speed = 5;
        Vector3 newCamPosition = newTransforms.position + offsetZoom;

        if (rotate)
        {
            Vector3 newCamDirection = newTransforms.position - transform.position;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, newCamDirection, 1 * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        else
        {
            transform.rotation = Quaternion.Euler(rotationInGame);
        }

        if (transform.position != newCamPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, newCamPosition, speed * Time.deltaTime);
        }
        else
        {
            isMoving = false;
        }

        return isMoving;
    }
}

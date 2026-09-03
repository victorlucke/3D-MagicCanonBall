using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public List<Transform> cutscenesTransform;
    [SerializeField] private bool isToChange;
    private Vector3 offset;
    private Vector3 rotationInGame;
    private InputAction changeCameraAction;
    [SerializeField] private bool isChangingPosition;

    void Awake()
    {
        offset = new Vector3(0, 10, -10);
        rotationInGame = new Vector3(45, 0, 0);
        changeCameraAction = InputSystem.actions.FindAction("Interact");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FindPlayer();
    }

    // Update is called once per frame
    void LateUpdate()
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
            FindPlayer();
    }

    void FindPlayer()
    {
        if (GameObject.FindWithTag("Player") != null)
        {
            player = GameObject.FindWithTag("Player").gameObject;
        }
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
    bool MoveCamera(Transform newTransforms, bool rotate)
    {
        bool isMoving = true;
        float speed = 5;
        Vector3 newCamPosition = newTransforms.position + offset;

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

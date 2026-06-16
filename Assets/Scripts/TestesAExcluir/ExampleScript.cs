using UnityEngine;
using UnityEngine.InputSystem;

public class ExampleScript : MonoBehaviour
{
    /// SCRIPT OF RAYCAST EXEMPLE. DRAW A LINE AND AIM IN FRONT OF CAMERA TO DETECT WALLS
    private float cameraRotation;

    void Start()
    {
        Camera.main.transform.position = new Vector3(0, 0.5f, 0);
        cameraRotation = 0.0f;
    }

    // Rotate the camera based on what the user wants to look at.
    // Avoid rotating more than +/-45 degrees.
    void OnMove(InputValue movement)
    {
        Vector2 movementVector = movement.Get<Vector2>();
        float horizontal = movementVector.x;

        if (horizontal > 0)
        {
            cameraRotation -= 1f;
            if (cameraRotation < -45.0f)
            {
                cameraRotation = -45.0f;
            }
        }

        if (horizontal < 0)
        {
            cameraRotation += 1f;
            if (cameraRotation > 45.0f)
            {
                cameraRotation = 45.0f;
            }
        }

        // Rotate the camera
        Camera.main.transform.localEulerAngles = new Vector3(0.0f, cameraRotation, 0.0f);

    }

    void FixedUpdate()
    {
        //Transform transform = Camera.main.transform;

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.yellow);

        if (InputSystem.actions.FindAction("Jump").IsPressed())
        {
            // Check for a Wall.
            LayerMask mask = LayerMask.GetMask("Wall");

            RaycastHit hit;

            // Check if a Wall is hit.
            if (Physics.Raycast(transform.position, transform.forward, out hit, 10f, mask))
            {
                Debug.Log("Fired and hit a wall. distance: " + hit.distance + ". Object: " + hit.collider.gameObject);
            }
        }
    }
}

using UnityEngine;

public class MoveCannon : MonoBehaviour
{
    public GameObject CannonMouth;
    public GameObject TireLeft;
    public GameObject TireRight;
    public float speed;
    public float tiredRotationSpeed;
    public float rotationSpeed;
    [SerializeField] private bool isMoveEnable;
    [SerializeField] private PlayerController playerController;
    private Rigidbody addedComponent;

    // Update is called once per frame
    void Update()
    {
        VerifyMoveCondition();
    }

    /// <summary>
    /// Verify wich direction object is moving and rotating based on axis value
    /// </summary>
    /// <param name="moveDirectionAxis">Position Axis used for moving foward, backyard (positive negative)</param>
    /// <param name="rotationDirectionAxis">Rotation axis for rotating left, right (positive negative)</param>
    void RotateTires(float moveDirectionAxis, float rotationDirectionAxis)
    {
        if (moveDirectionAxis > 0)
        {
            TireLeft.transform.Rotate(0, tiredRotationSpeed * Time.deltaTime, 0);
            TireRight.transform.Rotate(0, -tiredRotationSpeed * Time.deltaTime, 0);
        }
        else if (moveDirectionAxis < 0)
        {
            TireLeft.transform.Rotate(0, -tiredRotationSpeed * Time.deltaTime, 0);
            TireRight.transform.Rotate(0, tiredRotationSpeed * Time.deltaTime, 0);
        }

        if (rotationDirectionAxis > 0)
        {
            TireLeft.transform.Rotate(0, -tiredRotationSpeed * Time.deltaTime, 0);
            TireRight.transform.Rotate(0, -tiredRotationSpeed * Time.deltaTime, 0);
        }
        else if (rotationDirectionAxis < 0)
        {
            TireLeft.transform.Rotate(0, tiredRotationSpeed * Time.deltaTime, 0);
            TireRight.transform.Rotate(0, tiredRotationSpeed * Time.deltaTime, 0);
        }
    }

    /// <summary>
    /// Verify every condition to start moving
    /// </summary>
    void VerifyMoveCondition()
    {
        SearchPlayerControllerOnCannonMouth(CannonMouth);

        if (isMoveEnable)
        {
            float moveDirectionAxis = playerController.movementY;
            float rotateDirectionAxis = playerController.movementX;

            CreateEssentialComponent();

            if (VerifyIsMoving(moveDirectionAxis, rotateDirectionAxis))
            {
                Move(moveDirectionAxis);
                RotateTires(moveDirectionAxis, rotateDirectionAxis);
            }

            if(VeriftIsRotating(moveDirectionAxis, rotateDirectionAxis))
            {
                Rotate(rotateDirectionAxis);
                RotateTires(moveDirectionAxis, rotateDirectionAxis);
            }
        }
        else
        {
            if (addedComponent)
                DeleteEssentialComponent();

            playerController = null;
        }
    }

    /// <summary>
    /// Search for player controller in children object
    /// </summary>
    /// <param name="parentObject"></param>
    void SearchPlayerControllerOnCannonMouth(GameObject parentObject)
    {
        if (parentObject.transform.Find("Player"))
        {
            playerController = parentObject.transform.Find("Player").GetComponent<PlayerController>();

            isMoveEnable = true;
        }
        else
            isMoveEnable = false;

    }

    /// <summary>
    /// add components to enable this object to move case they dont exist
    /// </summary>
    void CreateEssentialComponent()
    {
        if (!GetComponent<Rigidbody>())
            addedComponent = gameObject.AddComponent<Rigidbody>();
    }

    /// <summary>
    /// allow movement if inst rotating
    /// </summary>
    /// <param name="moveAxisValue">Value representing direction of movement (positive negative)</param>
    /// <param name="rotateAxisValue">Value representing direction of rotation (positive negative)</param>
    /// <returns></returns>
    bool VerifyIsMoving(float moveAxisValue, float rotateAxisValue)
    {
        if (moveAxisValue != 0 && rotateAxisValue == 0)
        {
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// allow rotate if inst moving
    /// </summary>
    /// <param name="moveAxisValue">Value representing direction of movement (positive negative)</param>
    /// <param name="rotateAxisValue">Value representing direction of rotation (positive negative)</param>
    /// <returns></returns>
    bool VeriftIsRotating(float moveAxisValue, float rotateAxisValue)
    {
        if (rotateAxisValue != 0 && moveAxisValue == 0)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Delete the component in addedComponent in case it exist
    /// </summary>
    void DeleteEssentialComponent()
    {
        if (addedComponent != null)
            Destroy(addedComponent);
    }

    /// <summary>
    /// apply constant values on red axis, based on direction and speed
    /// </summary>
    /// <param name="moveDirection">Positive, negative value</param>
    void Move(float moveDirection)
    {
        transform.position += transform.right * -moveDirection * speed * Time.deltaTime;
    }

    /// <summary>
    /// apply constant value change on Z axis, based on speed
    /// </summary>
    /// <param name="rotateZ">positive, negative value</param>
    void Rotate(float rotateZ)
    {
        Vector3 rotateDirection = new Vector3(0, 0, rotateZ);

        transform.Rotate(rotateDirection * rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Rotate tires when moving or rotating, to create ilusion of movement
    /// </summary>
    /// <param name="moveDirection">positive negative values</param>
    /// <param name="rotateAxis">positive negative values</param>
    void TireRotate(float moveDirection, float rotateAxis)
    {
        RotateTires(moveDirection, rotateAxis);
    }
}

using UnityEngine;

public class MoveCannon : MonoBehaviour
{
    [Header("Essentials")]
    public GameObject CannonMouth;
    public GameObject TireLeft;
    public GameObject TireRight;
    public float speed;
    public float tiredRotationSpeed;
    public float rotationSpeed;
    public float limitRotateY;
    [Header("Permissions")]
    public bool moveEnabled;
    public bool rotateZEnabled;
    public bool rotateYEnabled;
    private bool isMoveEnable;
    private PlayerController playerController;
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

            //CreateEssentialComponent();

            if (moveEnabled && VerifyIsMoving(moveDirectionAxis, rotateDirectionAxis))
            {
                Move(moveDirectionAxis);
                RotateTires(moveDirectionAxis, rotateDirectionAxis);
            }
            else if (rotateYEnabled && VerifyIsMoving(moveDirectionAxis, rotateDirectionAxis))
            {
                RotateY(moveDirectionAxis);
            }

            if (rotateZEnabled && VeriftIsRotating(moveDirectionAxis, rotateDirectionAxis))
            {
                Rotate(0, 0, rotateDirectionAxis);
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
        string childTag = null;
        
        if (parentObject.transform.childCount >= 1)
            childTag = parentObject.transform.GetChild(0).tag;

        if (childTag == "Player")
        {
            playerController = parentObject.transform.GetComponentInChildren<PlayerController>();

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
    /// <param name="rotateX">positive, negative value</param>
    /// <param name="rotateY">positive, negative value</param>
    /// <param name="rotateZ">positive, negative value</param>
    void Rotate(float rotateX, float rotateY, float rotateZ)
    {
        Vector3 rotateDirection = new Vector3(rotateX, rotateY, rotateZ);
        Vector3 currentRotationAngle = rotateDirection * rotationSpeed * Time.deltaTime;

        transform.Rotate(currentRotationAngle);
    }

    /// <summary>
    /// rotate in Y axis in a max of "0 and limitRotateY" degress 
    /// </summary>
    /// <param name="rotateY">direction +- of the movement</param>
    void RotateY(float rotateY)
    {
        bool isUp;
        Vector3 rotateAxisY = new Vector3(0, rotateY, 0);
        float currentEulerY = transform.localRotation.eulerAngles.y;

        if (rotateY > 0)
            isUp = true;
        else
            isUp = false;

        //move upward
        if (isUp && (currentEulerY < limitRotateY || currentEulerY > 300))
        {
            transform.Rotate(rotateAxisY * rotationSpeed * Time.deltaTime);
        }
        else if (isUp && currentEulerY > limitRotateY)
        {
            Vector3 lockRotation = transform.localEulerAngles;
            lockRotation.y = limitRotateY;
            transform.localEulerAngles = lockRotation;
        }

        //move downward
        if (!isUp && currentEulerY <= limitRotateY + .5)
        {
            transform.Rotate(rotateAxisY * rotationSpeed * Time.deltaTime);
        }
        else if (!isUp && currentEulerY > limitRotateY)
        {
            Vector3 lockRotation = transform.localEulerAngles;
            lockRotation.y = 0;
            transform.localEulerAngles = lockRotation;
        }
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

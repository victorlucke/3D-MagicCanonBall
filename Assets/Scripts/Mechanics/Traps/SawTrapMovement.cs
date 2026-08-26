using UnityEngine;

public class SawTrapMovement : MonoBehaviour
{
    public GameObject SawBase;
    public GameObject Saw;
    public float moveSpeed;
    public enum ChoseAxis {x,y,z}
    public ChoseAxis choseAxisToMove;
    Vector3 objectSize;
    float objectLength;
    bool isGoingFoward;

    void Awake()
    {
        objectSize = SawBase.GetComponent<Renderer>().bounds.size;
        objectLength = objectSize.z;
    }

    // Update is called once per frame
    void Update()
    {
        MoveSaw(objectLength, choseAxisToMove);
    }

    void MoveSaw(float lengthToCover, ChoseAxis axisToMove)
    {
        float halfLength = lengthToCover / 2;

        if (axisToMove == ChoseAxis.x)
        {
            if (isGoingFoward && Saw.transform.localPosition.x < halfLength)
            {
                Saw.transform.localPosition += Vector3.right * moveSpeed * Time.deltaTime;
            }
            else if (isGoingFoward && Saw.transform.localPosition.x >= halfLength)
            {
                isGoingFoward = false;
            }

            if (!isGoingFoward && Saw.transform.localPosition.x > -halfLength)
            {
                Saw.transform.localPosition += -Vector3.right * moveSpeed * Time.deltaTime;
            }
            else if (!isGoingFoward && Saw.transform.localPosition.x <= -halfLength)
            {
                isGoingFoward = true;
            }
        }
        else if (axisToMove == ChoseAxis.y)
        {
            if (isGoingFoward && Saw.transform.localPosition.y < halfLength)
            {
                Saw.transform.localPosition += Vector3.up * moveSpeed * Time.deltaTime;
            }
            else if (isGoingFoward && Saw.transform.localPosition.y >= halfLength)
            {
                isGoingFoward = false;
            }

            if (!isGoingFoward && Saw.transform.localPosition.y > -halfLength)
            {
                Saw.transform.localPosition += -Vector3.up * moveSpeed * Time.deltaTime;
            }
            else if (!isGoingFoward && Saw.transform.localPosition.y <= -halfLength)
            {
                isGoingFoward = true;
            }
        }
        else if (axisToMove == ChoseAxis.z)
        {
            if (isGoingFoward && Saw.transform.localPosition.z < halfLength)
            {
                Saw.transform.localPosition += Vector3.forward * moveSpeed * Time.deltaTime;
            }
            else if (isGoingFoward && Saw.transform.localPosition.z >= halfLength)
            {
                isGoingFoward = false;
            }

            if (!isGoingFoward && Saw.transform.localPosition.z > -halfLength)
            {
                Saw.transform.localPosition += -Vector3.forward * moveSpeed * Time.deltaTime;
            }
            else if (!isGoingFoward && Saw.transform.localPosition.z <= -halfLength)
            {
                isGoingFoward = true;
            }
        }
    }
}

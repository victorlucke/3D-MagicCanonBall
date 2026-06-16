using System;
using Unity.VisualScripting;
using UnityEngine;

public class ColliderDetector : MonoBehaviour
{
    public Transform aimPoint;
    public LayerMask layerToDetect;



    public GameObject ReturnDetectCollider()
    {
        RaycastHit hit;
        if (Physics.Raycast(aimPoint.position, aimPoint.TransformVector(Vector3.forward), out hit, 100, layerToDetect))
        {
            if (hit.distance < 10)
            {
                GameObject breakableObject;

                breakableObject = hit.collider.gameObject;

                return breakableObject;
            }
            else
                return null;
        }
        else
            return null;
        //if(Physics.Raycast(transform.position, transform.forward))
    }

    public void DetectCollider()
    {
        RaycastHit hit;
        if (Physics.Raycast(aimPoint.position, aimPoint.TransformVector(Vector3.forward), out hit, 100, layerToDetect))
        {
            if (hit.distance < 10)
            {
                GameObject breakableObject;

                breakableObject = hit.collider.gameObject;

                breakableObject.GetComponent<BreakObject>().IdentifyBreakObjects();
            }
        }
        //if(Physics.Raycast(transform.position, transform.forward))
    }

    void FixedUpdate()
    {
        DetectCollider();
    }
}

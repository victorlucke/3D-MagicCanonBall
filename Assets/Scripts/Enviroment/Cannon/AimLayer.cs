using UnityEngine;

public class AimLayer : MonoBehaviour
{
     public Transform aimPoint;
    public LayerMask layerToDetect;



    public GameObject ReturnDetectObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(aimPoint.position, aimPoint.TransformVector(Vector3.forward), out hit, 100, layerToDetect))
        {
            if (hit.distance < 10)
            {
                GameObject objectDetected;

                objectDetected = hit.collider.gameObject;

                return objectDetected;
            }
            else
                return null;
        }
        else
            return null;
        //if(Physics.Raycast(transform.position, transform.forward))
    }

    /// <summary>
    /// Check the object layer in front of aimPoint, and trigger GameEvent passing this object.
    /// </summary>
    public void DetectObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(aimPoint.position, aimPoint.TransformVector(Vector3.forward), out hit, 100, layerToDetect))
        {
            if (hit.distance < 10)
            {
                GameObject objectDetected;

                objectDetected = hit.collider.gameObject;

                GameEvents.TriggerOnAimingLayer(objectDetected);
            }
        }else
            GameEvents.TriggerOnAimingLayer(null);
        //if(Physics.Raycast(transform.position, transform.forward))
    }

    void FixedUpdate()
    {
        DetectObject();
    }
}

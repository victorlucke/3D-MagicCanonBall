using UnityEngine;
using UnityEngine.InputSystem;

public class Cannon : MonoBehaviour
{
    InputAction interactAction;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        
    }

    bool VerifyShootable(GameObject shootObject)
    {
        bool isShootable;

        if (shootObject.CompareTag("player"))
            isShootable = true;
        else
            isShootable = false;

        return isShootable;
    }

    public void ReloadCannon(GameObject ammunition)
    {
        Vector3 cannonMouthPosition;

        cannonMouthPosition = transform.Find("CannonMouth").position;

        if (VerifyShootable(ammunition))
        {
            if (interactAction.IsPressed())
            {
                StopAmmunition(ammunition);
                
            }
        }
    }

    public void StopAmmunition(GameObject motionAmmo)
    {
        Rigidbody ammoRigidbody;

        if(motionAmmo.GetComponent<Rigidbody>())
        {
            ammoRigidbody = motionAmmo.GetComponent<Rigidbody>();

            ammoRigidbody.linearVelocity = Vector3.zero;
            ammoRigidbody.angularVelocity = Vector3.zero;

            ammoRigidbody.useGravity = false;
        }
    }
}

using UnityEngine;

public class SlowProjectileZone : MonoBehaviour
{
    public float slowDownSpeed;
    Rigidbody otherRigidBody;
    [SerializeField] private bool movingProjectile;

    void OnTriggerEnter(Collider other)
    {
        movingProjectile = true;

        if (other.gameObject.GetComponent<Rigidbody>())
            otherRigidBody = other.gameObject.GetComponent<Rigidbody>();
    }

    void OnTriggerStay(Collider other)
    {
        if (movingProjectile && otherRigidBody)
        {
            SlowDownZone(otherRigidBody);

            if (otherRigidBody.linearVelocity == Vector3.zero)
                movingProjectile = false;
        }
    }

    /// <summary>
    /// Gradualy slow object based on his current magnitude over time until velocity is zero
    /// </summary>
    /// <param name="rigidBodyToSlow">object rigidBody</param>
    void SlowDownZone(Rigidbody rigidBodyToSlow)
    {
        Vector3 otherLinearVelocity = rigidBodyToSlow.linearVelocity;
        float otherSpeed = otherLinearVelocity.magnitude;

        if (otherLinearVelocity.magnitude > 5)
        {
            rigidBodyToSlow.linearVelocity = Vector3.MoveTowards(otherLinearVelocity, Vector3.zero, otherSpeed * slowDownSpeed * Time.deltaTime);
        }
        else
            rigidBodyToSlow.linearVelocity = Vector3.zero;
    }
}

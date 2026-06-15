using System.Buffers.Text;
using UnityEngine;

public class FloorSand : Floors
{
    public float dampingForce;
    private bool isDebufApplied;
    void Awake()
    {
        thisFloorType = FloorType.Sand;
        dampingForce = 3;
    }

    protected override void FloorCondition()
    {
        if(playerObject != null && !isDebufApplied)
        {
            Rigidbody playerRigidyBody;
            playerRigidyBody = playerObject.GetComponent<Rigidbody>();

            playerRigidyBody.linearDamping += dampingForce;

            isDebufApplied = true;
        }
    }

    void RevertCondition()
    {
        if(playerObject != null && isDebufApplied)
        {
            Rigidbody playerRigidyBody;
            playerRigidyBody = playerObject.GetComponent<Rigidbody>();

            playerRigidyBody.linearDamping -= dampingForce;

            isDebufApplied = false;
            playerObject = null;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        RevertCondition();
    }
}

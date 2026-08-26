using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Floors : MonoBehaviour
{
    public enum FloorType { Sand, Stone, Wood, Metal }
    public FloorType thisFloorType;
    protected GameObject playerObject;

    protected virtual void FloorCheck()
    {
        FloorCondition();
    }

    protected abstract void FloorCondition();

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
            playerObject = collision.gameObject;
    }

    protected void OnCollisionStay(Collision collision)
    {
        FloorCheck();
    }
}

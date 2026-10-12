using UnityEngine;

public class Skeleton : NavMeshMovement
{
    [SerializeField] Transform lastParent;
    [SerializeField] Transform moveLocationDirection;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {

    }

    protected override void Update()
    {
        NewDiversionDestination = ReturnMoveLocationInHierarchy(currentTargetDetected);
        base.Update();
    }

    /// <summary>
    /// Search the parents of target, if find it return the MoveLocation Object to stop pursuing target to go toward MoveLocation
    /// </summary>
    /// <param name="myCurrentTarget"></param>
    /// <returns></returns>
    private Transform ReturnMoveLocationInHierarchy(GameObject myCurrentTarget)
    {

        if (myCurrentTarget)
        {
            lastParent = myCurrentTarget.transform;
            
            while (lastParent.parent && !lastParent.Find("MoveLocation"))
            {
                lastParent = lastParent.parent;
            }

            if (lastParent.Find("MoveLocation"))
            {
                moveLocationDirection = lastParent.Find("MoveLocation");
                return moveLocationDirection;
            }
        }

        // if (myCurrentTarget)
        // {
        //     parentTransformInTarget = myCurrentTarget.transform;

        //     if (parentTransformInTarget.parent)
        //     {
        //         grandParentTransformInTarget = parentTransformInTarget.parent.transform;

        //         parentTransformInTarget.

        //         if (grandParentTransformInTarget.Find("MoveLocation"))
        //         {
        //             moveLocationDirection = grandParentTransformInTarget.Find("MoveLocation");
        //             return moveLocationDirection;
        //         }
        //     }
        // }

        return null;
    }
}
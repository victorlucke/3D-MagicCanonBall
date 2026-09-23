using UnityEngine;

/// <summary>
/// this classe exist as a essential part of "SeeTarget" Class, it represents the range of vision, and announces
/// every time it sees the target inside the vision range
/// </summary>
public class VisionRangeTrigger : MonoBehaviour
{
    private SeeTarget seeTargetComponent;
    private GameObject currentTargetObject;
    private bool isInRange;
    void Start()
    {
        //Verify if essential component exist in parent
        if (GetComponentInParent<SeeTarget>())
            seeTargetComponent = GetComponentInParent<SeeTarget>();
        else
            Debug.Log("Essential component donst exist 'SeeTarget'");
    }

    void Update()
    {
        if (seeTargetComponent)
        {
            if (!currentTargetObject)
                if (isInRange)
                    isInRange = false;

            if (seeTargetComponent.targetInRange != isInRange)
                seeTargetComponent.targetInRange = isInRange;

            if (seeTargetComponent.targetObject != currentTargetObject)
                seeTargetComponent.targetObject = currentTargetObject;
        }
    }

    /// <summary>
    /// responsible to announce to parent when target is inside vision range
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerStay(Collider other)
    {
        if (seeTargetComponent)
        {
            if (other.CompareTag(seeTargetComponent.TargetTag))
            {
                isInRange = true;
                currentTargetObject = other.gameObject;
            }

        }
    }

    /// <summary>
    /// responsible to announce to parent when the target isnt in vision range anymore
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit(Collider other)
    {
        if (seeTargetComponent)
        {
            if (other.CompareTag(seeTargetComponent.TargetTag))
            {
                isInRange = false;
                currentTargetObject = null;
            }
        }
    }
}

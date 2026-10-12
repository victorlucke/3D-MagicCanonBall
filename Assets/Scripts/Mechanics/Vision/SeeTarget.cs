using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// remember to add more rays in LookAtTarget, in each corner of the target. Top Bot Left Right, soo you can try to see it not only
/// by its center.
/// </summary>
public abstract class SeeTarget : MonoBehaviour
{
    [Header("SeeTarget Class")]
    //[SerializeField] private LayerMask targetLayer;
    [Tooltip("Prefab representing tag to search")]
    [SerializeField] private GameObject targetPrefabToSee;
    [SerializeField] private LayerMask layersBlockingVision;
    [SerializeField] private float visionRange;
    [SerializeField] private GameObject eyeObj;
    public bool sawEnemy { get { return _sawEnemy; } }
    public bool blokedVision { get { return _blokedVision; } set { _blokedVision = value; } }
    protected GameObject currentTargetDetected { get { return detectionArea.objectTargetDetected; } }
    private bool _sawEnemy;
    private bool _blokedVision;
    private LayerMask layersToCheck;
    private DetectionArea detectionArea;

    // private bool _targetInRange;
    // public bool targetInRange { get { return _targetInRange; } set { _targetInRange = value; } }
    // private GameObject _targetObject;
    //  public GameObject targetObject { get { return _targetObject; } set { _targetObject = value; } }

    protected virtual void Awake()
    {
        detectionArea = new DetectionArea().CreateDetectionArea(gameObject.transform, visionRange, targetPrefabToSee);
        //Debug.Log("Layer "+( 1 << targetPrefabToSee.layer) + " / " +targetLayer.value);
    }

    protected virtual void Update()
    {
        LookAtTarget(detectionArea, eyeObj, layersToCheck, _blokedVision);
    }

    /// <summary>
    /// launch an ray in direction of target, if there isnt any obstacle in the way layersBlockingVision it sees the target
    /// </summary>
    protected void LookAtTarget(DetectionArea myDetectionArea, GameObject myEyesObj, LayerMask myLayersToCheck, bool isVisionBloked)
    {
        AddLayersToSee();

        CheckBlockedVision();

        if (myDetectionArea.isInRange && myDetectionArea.objectTargetDetected && !isVisionBloked)
        {
            Vector3 EyePosition = myEyesObj.transform.position;
            Vector3 directionToLook = myDetectionArea.objectTargetDetected.transform.position - EyePosition;
            RaycastHit hit;

            bool isInVisionRay = Physics.Raycast(EyePosition, directionToLook, out hit, 30f, myLayersToCheck);

            //DrawRayLine(hit, EyePosition, directionToLook);

            if (isInVisionRay)
            {
                if (hit.collider.gameObject.layer == myDetectionArea.objectTargetDetected.layer)
                    _sawEnemy = true;
                else
                    _sawEnemy = false;
            }
        }
        else _sawEnemy = false;
    }

    protected void LookAtTarget() => LookAtTarget(detectionArea, eyeObj, layersToCheck, _blokedVision);

    /// <summary>
    /// Draw line until hit a Breakable Layer or the directionToLook
    /// </summary>
    /// <param name="myHit">The detection to check brekable layer</param>
    /// <param name="myStartPoint"></param>
    /// <param name="myDirectionToLook"></param>
    void DrawRayLine(RaycastHit myHit, Vector3 myStartPoint, Vector3 myDirectionToLook)
    {
        if (myHit.collider.gameObject.layer == 13)
        {
            myDirectionToLook = myHit.collider.gameObject.transform.position - myStartPoint;
            Debug.DrawRay(myStartPoint, myDirectionToLook, Color.red, 30f);
        }
        else Debug.DrawRay(myStartPoint, myDirectionToLook, Color.red, 30f);
    }

    /// <summary>
    /// add the target and blockinVision layers to simulate the vision toward target
    /// </summary>
    void AddLayersToSee()
    {
        // check if target layer exist
        if ((1 << targetPrefabToSee.layer) != 0)
        {
            //check if layers blocking vision exist and if is alread added
            if (layersBlockingVision.value != 0 && (layersToCheck.value & layersBlockingVision.value) == 0)
            {
                // add target layer and the layer blockingVision as layerMask.value (bit value) converted to its logaritmin to add it to layersToCheck
                layersToCheck = LayerMask.GetMask(LayerMask.LayerToName(targetPrefabToSee.layer), LayerMask.LayerToName((int)Mathf.Log(layersBlockingVision.value, 2)));
                //Debug.Log("layer 1 value " + layersToCheck.value);
            }
            //add only the target layer if inst there
            else if ((layersToCheck.value & (1 << targetPrefabToSee.layer)) == 0) layersToCheck = LayerMask.GetMask(LayerMask.LayerToName(targetPrefabToSee.layer));
        }
        else
        {
            Debug.Log("Target layer donst exist in " + gameObject.name);
        }
    }

    /// <summary>
    /// Check if fisical effect blind was applyied
    /// </summary>
    private void CheckBlockedVision()
    {
        if (FisicalEffectsManager.Instance.AffectedObjects.ContainsKey(gameObject))
        {
            _blokedVision = true;
            //Debug.Log("target blind");
        }
        else
        {
            //Debug.Log("target not blind");
            _blokedVision = false;
        }
    }
}

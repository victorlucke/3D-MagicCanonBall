using System;
using UnityEngine;

/// <summary>
/// remember to add more rays in LookAtTarget, in each corner of the target. Top Bot Left Right, soo you can try to see it not only
/// by its center.
/// </summary>
public abstract class SeeTarget : MonoBehaviour
{
    [Header("SeeTarget Class")]
    [Tooltip("Object representing tag to search")]
    public GameObject TargetPrefabToSee;
    [SerializeField] private LayerMask TargetLayer;
    [SerializeField] private LayerMask LayersBlockingVision;
    [SerializeField] private float VisionRange;
    [SerializeField] private GameObject EyeObj;
    public bool sawEnemy { get { return _sawEnemy; } }
    public bool targetInRange { get { return _targetInRange; } set { _targetInRange = value; } }
    public GameObject targetObject { get { return _targetObject; } set { _targetObject = value; } }
    [SerializeField] private bool _sawEnemy;
    [SerializeField] private bool _targetInRange;
    [SerializeField] private LayerMask layersToCheck;
    private GameObject _targetObject;
    private String nameOfRangeDetector;

    protected virtual void Update()
    {
        LookAtTarget();
    }

    /// <summary>
    /// Do all the essentials for SeeTarget Work
    /// </summary>
    protected void StartSeeTarget()
    {
        nameOfRangeDetector = "VisionRangeDetector";

        CreateDistanceDetector(nameOfRangeDetector, gameObject.transform);
    }

    /// <summary>
    /// launch an ray in direction of target, if there isnt any obstacle in the way LayersBlockingVision it sees the target
    /// </summary>
    protected void LookAtTarget()
    {
        AddLayersToSee();

        if (_targetInRange && _targetObject)
        {
            Vector3 EyePosition = EyeObj.transform.position;
            Vector3 directionToLook = _targetObject.transform.position - EyePosition;
            RaycastHit hit;

            bool isInVisionRay = Physics.Raycast(EyePosition, directionToLook, out hit, 30f, layersToCheck);

            Debug.DrawRay(EyePosition, directionToLook, Color.red, 30f);

            if (isInVisionRay)
            {
                if (hit.collider.gameObject.layer == (int)Mathf.Log(TargetLayer.value, 2))
                    _sawEnemy = true;
                else
                    _sawEnemy = false;
            }
        } else _sawEnemy = false;
    }

    /// <summary>
    /// add the target and blockinVision layers to simulate the vision toward target
    /// </summary>
    void AddLayersToSee()
    {
        // check if target layer exist
        if (TargetLayer.value != 0)
        {
            //check if layers blocking vision exist and if is alread added
            if (LayersBlockingVision.value != 0 && (layersToCheck.value & LayersBlockingVision.value) == 0)
            {
                // add each layerMask.value (bit value) converted to its logaritmin to add it to layersToCheck
                layersToCheck = LayerMask.GetMask(LayerMask.LayerToName((int)Mathf.Log(TargetLayer.value, 2)), LayerMask.LayerToName((int)Mathf.Log(LayersBlockingVision.value, 2)));
                //Debug.Log("layer 1 value " + layersToCheck.value);
            }
            //add only the target layer if inst there
            else if ((layersToCheck.value & TargetLayer.value) == 0) layersToCheck = LayerMask.GetMask(LayerMask.LayerToName((int)Mathf.Log(TargetLayer.value, 2)));
        }
        else
        {
            Debug.Log("Target layer donst exist in "+ gameObject.name);
        }
    }

    /// <summary>
    /// Create the collider responsible to simulate vision range, and return the Game Object that contains It. 
    /// </summary>
    /// <param name="detectorName">name of object that will contain the collider</param>
    /// <param name="parentTransform">parent of the object that contain the collider</param>
    /// <returns></returns>
    private void CreateDistanceDetector(String detectorName, Transform parentTransform)
    {
        Debug.Log("CreatingRangeDetector");
        if (!parentTransform.Find(detectorName))
        {
            GameObject detector = new GameObject(detectorName);
            
            detector.transform.position = transform.position;
            detector.transform.SetParent(transform);


            SphereCollider detectorCollider = detector.AddComponent<SphereCollider>();

            //detector.name = detectorName;

            detectorCollider.isTrigger = true;
            detectorCollider.radius = VisionRange;

            //Instantiate(detector, parentTransform.position, parentTransform.rotation, parentTransform);

            detector.AddComponent<VisionRangeTrigger>();
        }
    }
}

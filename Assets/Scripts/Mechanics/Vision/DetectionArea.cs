using System;
using System.Collections;
using UnityEngine;

public class DetectionArea : MonoBehaviour
{
    public GameObject objectTargetDetected { get { return _objectTargetDetected; } }
    public bool isInRange { get { return _isInRange; } }
    public GameObject targetReference { set { _targetReference = value; } }
    [SerializeField] private GameObject _objectTargetDetected;
     private GameObject _targetReference;
    [SerializeField] private bool _isInRange;

    void Start()
    {
        StartCoroutine(DetectError());
    }

    void Update()
    {
        if (!_objectTargetDetected)
            if (_isInRange)
                _isInRange = false;
    }

    // THIS METHOS IS CALLED INSIDE ANOTHER SCRIPTS THAT INTEND TO USE DETECTION
    /// <summary>
    /// Create all essencial to Detect an Target inside an area (Sphere collider and DetectionArea script as component)
    /// </summary>
    /// <param name="parentTransform">Transform of parent to create the detection area inside</param>
    /// <param name="areaRange">Area of detection</param>
    /// <param name="targetReference">Target to detect (preferably an prefab)</param>
    /// <returns></returns>
    public DetectionArea CreateDetectionArea(Transform parentTransform, float areaRange, GameObject targetReference)
    {
        String areaName;
        areaName = parentTransform.gameObject.name + "_DetectiorArea";
        //Debug.Log("CreatingRangeDetector");
        if (!parentTransform.Find(areaName))
        {
            DetectionArea newDetectionArea;
            GameObject detector = new GameObject(areaName);

            detector.transform.position = parentTransform.position;
            detector.transform.SetParent(parentTransform);


            SphereCollider detectorCollider = detector.AddComponent<SphereCollider>();

            //detector.name = AreaName;

            detectorCollider.isTrigger = true;
            detectorCollider.radius = areaRange;

            newDetectionArea = detector.AddComponent<DetectionArea>();
            newDetectionArea.targetReference = targetReference;

            //Instantiate(detector, parentTransform.position, parentTransform.rotation, parentTransform);
            return newDetectionArea;
        }
        else
        {
            DetectionArea oldDetectionArea;

            oldDetectionArea = parentTransform.Find(areaName).GetComponent<DetectionArea>();
            
            return oldDetectionArea;
        }
    }

    /// <summary>
    /// Detect missing essential data
    /// </summary>
    IEnumerator DetectError()
    {
        yield return new WaitForSeconds(1);

        if (!_targetReference) Debug.Log(gameObject.transform.parent.name + " " + gameObject.name + " Target Reference is missing");
    }

    /// <summary>
    /// responsible to announce to parent when target is inside vision range
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerStay(Collider other)
    {
        if (_targetReference)
        {
            if (other.CompareTag(_targetReference.tag))
            {
                _isInRange = true;
                _objectTargetDetected = other.gameObject;
            }
        }
    }

    /// <summary>
    /// responsible to announce to parent when the target isnt in vision range anymore
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit(Collider other)
    {
        if (_targetReference)
        {
            if (other.CompareTag(_targetReference.tag))
            {
                _isInRange = false;
                _objectTargetDetected = null;
            }
        }
    }
}

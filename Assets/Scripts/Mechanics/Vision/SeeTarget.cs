using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeTarget : MonoBehaviour
{
    public float VisionRange;
    public GameObject EyeObj;
    public String TargetTag;
    public bool sawEnemy { get { return _sawEnemy; } }
    public bool targetInRange { get { return _targetInRange; } set { _targetInRange = value; } }
    public GameObject targetObject { get { return _targetObject; } set { _targetObject = value; } }
    private bool _sawEnemy;
    private bool _targetInRange;
    private GameObject _targetObject;
    private String nameOfRangeDetector;

    //"trash"
    private Coroutine coroutineOnceLookForTarget;
    private Coroutine coroutineOnce;

    void Awake()
    {
        nameOfRangeDetector = "VisionRangeDetector";
    }

    void Start()
    {
        CreateDistanceDetector(nameOfRangeDetector, transform);
    }

    void Update()
    {
        if (_targetInRange)
            Debug.Log("target in range");
        if (_targetObject)
            Debug.Log("Target name "+_targetObject.name);
    }

    void CheckTargetDistance()
    {

    }

    /// <summary>
    /// Create the collider responsible to simulate vision range, and return the Game Object that contains It. 
    /// </summary>
    /// <param name="detectorName">name of object that will contain the collider</param>
    /// <param name="parentTransform">parent of the object that contain the collider</param>
    /// <returns></returns>
    private void CreateDistanceDetector(String detectorName, Transform parentTransform)
    {
        if (!parentTransform.Find(detectorName))
        {
            GameObject detector = new GameObject();
            SphereCollider detectorCollider = detector.AddComponent<SphereCollider>();

            detector.name = detectorName;

            detectorCollider.isTrigger = true;
            detectorCollider.radius = VisionRange;

            detector.AddComponent<VisionRangeTrigger>();

            Instantiate(detector, parentTransform.position, parentTransform.rotation, parentTransform);
        }
    }

    IEnumerator showList(List<GameObject> myList)
    {
        foreach (GameObject obj in myList)
        {
            Debug.Log(obj.name);
            yield return null;
        }

        coroutineOnce = null;
    }

    // void LookForTarget()
    // {
    //     List<GameObject> listTargetObjects = new List<GameObject>();
    //     int amountOfTargetObjects = GameObject.FindGameObjectsWithTag(TargetTag).Length;

    //     if (listTargetObjects.Count < amountOfTargetObjects)
    //     {
    //         GameObject.FindGameObjectsWithTag(TargetTag, listTargetObjects);
    //     }
    // }

    // IEnumerator LateLookForTarget()
    // {
    //     //float maxTimeExecution = 10;
    //     float timePassed = 0;

    //     while (!sawEnemy)
    //     {
    //         timePassed += Time.deltaTime;
    //         RaycastHit hit;
    //         Vector3 rayStartPosition = EyeObj.transform.position;
    //         Vector3 rayDirection = EyeObj.transform.forward;

    //         Debug.Log("running");

    //         for (int i = -PeripherialRange / 2; i < PeripherialRange / 2; i++)
    //         {
    //             Vector3 peripherialVision = new Vector3(i, 0, 0);
    //             Debug.Log(-PeripherialRange / 2 + " " + PeripherialRange / 2);

    //             Debug.DrawRay(rayStartPosition, (rayDirection * VisionRange) + peripherialVision, Color.red);
    //         }

    //         if (Physics.Raycast(rayStartPosition, rayDirection, out hit, VisionRange))
    //         {
    //             if (hit.transform.CompareTag(TargetTag))
    //             {
    //                 Debug.Log("found target");
    //             }
    //         }
    //         yield return null;
    //     }

    //     coroutineOnceLookForTarget = null;
    // }
}

using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ThrowObjects : MonoBehaviour
{
    public GameObject ObjctThrowed;
    public GameObject SpawnPoint;
    public float arcHeight = 5.0f;
    public float duration = 2.0f;
    public float ThrowWaitTime;
    private GameObject target;
    private GameObject objectInstance;
    private InputAction pressE;
    private bool isRunning;

    void Awake()
    {
        pressE = InputSystem.actions.FindAction("Interact");
    }

    void Update()
    {
        if (!objectInstance)
            objectInstance = SpawnObject(ObjctThrowed, SpawnPoint);
        else if (!objectInstance.transform.parent)
            objectInstance = SpawnObject(ObjctThrowed, SpawnPoint);

        if (!target)
            target = FindTarget("Player");

        if (target && objectInstance)
            ThrowObject(objectInstance, target.transform.position);

    }

    /// <summary>
    /// Start the movement of an object
    /// </summary>
    /// <param name="myObj">reference to object in scene</param>
    void ThrowObject(GameObject throwedObject, Vector3 targetLocation)
    {
        if (!isRunning && throwedObject.transform.parent)
        {
            throwedObject.transform.SetParent(null);
            StartCoroutine(MoveInArc(throwedObject.transform.position, targetLocation, objectInstance));
        }
    }

    /// <summary>
    /// search for an object with the tag in the scene
    /// </summary>
    /// <param name="tagToFind">Tag of the target</param>
    GameObject FindTarget(string tagToFind)
    {
        GameObject targetObject = GameObject.FindWithTag("Player");
        return targetObject;
    }

    /// <summary>
    /// spawn an object in the scene
    /// </summary>
    /// <param name="objectToSpawn">the prefab reference</param>
    /// <param name="objectAsLocation">An empty Object representing the location to spawn and parent with</param>
    GameObject SpawnObject(GameObject objectToSpawn, GameObject objectAsLocation)
    {
        GameObject newObject;
        newObject = Instantiate(objectToSpawn, objectAsLocation.transform.position, objectAsLocation.transform.rotation);
        newObject.transform.SetParent(objectAsLocation.transform);
        return newObject;
        // }
    }

    /// <summary>
    /// Traverse the object in a archline between start and end points
    /// </summary>
    /// <param name="startPoint"></param>
    /// <param name="endPoint"></param>
    /// <param name="MyObject">reference to the object in scene</param>
    /// <returns></returns>
    IEnumerator MoveInArc(Vector3 startPoint, Vector3 endPoint, GameObject MyObject)
    {
        if (MyObject)
        {
            isRunning = true;
            float timeElapsed = 0f;

            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                float linearProgress = timeElapsed / duration;

                // 1. Linearly interpolate the X and Z positions
                Vector3 currentPos = Vector3.Lerp(startPoint, endPoint, linearProgress);

                // 2. Calculate the parabolic height offset
                // Formula: 4 * height * progress * (1 - progress)
                float heightOffset = 4f * arcHeight * linearProgress * (1f - linearProgress);

                // 3. Apply the height to the object
                currentPos.y += heightOffset;
                MyObject.transform.position = currentPos;

                yield return null;
            }

            // Ensure final position snaps accurately to the target
            MyObject.transform.position = endPoint;
            MyObject.transform.rotation = Quaternion.Euler(Vector3.zero);

            yield return new WaitForSeconds(ThrowWaitTime);

            isRunning = false;
        }
    }
}

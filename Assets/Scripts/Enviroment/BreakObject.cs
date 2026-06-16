using FirstGearGames.SmoothCameraShaker;
using Unity.VisualScripting;
using UnityEngine;

public class BreakObject : MonoBehaviour
{
    public bool isInsideParents;
    public GameObject[] objectsToBreak;

     /// <summary>
    /// search and identify wich objects are the breakable inside the array. depending if isInsideParents or not.
    /// after identifying them, call insert break components
    /// </summary>
    public void IdentifyBreakObjects()
    {
        //check every objects inside array
        foreach (GameObject obj in objectsToBreak)
        {
            // for child objects, check every object inside parent
            if (isInsideParents)
            {
                int childNumber = obj.transform.childCount;
                GameObject[] childGameObjects = new GameObject[childNumber];

                for (int i = 0; i < childGameObjects.Length; i++)
                {
                    childGameObjects[i] = obj.transform.GetChild(i).gameObject;
                    
                    InsertBreakComponents(childGameObjects[i]);
                }
            }
            else if (!isInsideParents)
            {
                InsertBreakComponents(obj);
            }
        }
    }

    /// <summary>
    /// try to activate or create components rigidbody and collider insde a breakable object to allow physics
    /// </summary>
    /// <param name="myObj"></param>
    void InsertBreakComponents(GameObject myObj)
    {
        //check if rigidbody exist to disable kinematic, else creates it
        if (myObj.GetComponent<Rigidbody>() == null)
            myObj.AddComponent<Rigidbody>();
        else
            myObj.GetComponent<Rigidbody>().isKinematic = false
            ;
        //check if collider exist to enable, else creates it
        if (myObj.GetComponent<Collider>() == null)
            myObj.AddComponent<MeshCollider>();
        else
            myObj.GetComponent<Collider>().enabled = true;
    }

}

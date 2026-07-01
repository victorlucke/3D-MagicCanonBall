using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : BasicFunctionalities
{
    public bool isInsideParents;
    public GameObject[] objectsToBreak;
    public List<string> tagOfObjectBreakers;
    public bool isToBreak;
    public bool IsTargtingMe;
    public void VerifyIsTargtingMe(GameObject currentTarget)
    {
        IsTargtingMe = currentTarget == gameObject;
    }

    public void VerifyIsToBreak(GameObject objectColliding)
    {
        if (IsTargtingMe && tagOfObjectBreakers.Contains(objectColliding.tag))
            isToBreak = true;

    }

    void OnCollisionEnter(Collision collision)
    {
        if (isToBreak)
        {
            StartBreak();
        }
    }

    /// <summary>
    /// call the necessary function to make object breakable
    /// </summary>
    public void StartBreak()
    {
        PlaySoundEffect();
        IdentifyBreakObjects();
        StartCoroutine(WaitToRemoveParentCollider());
    }

    private IEnumerator WaitToRemoveParentCollider()
    {
        yield return new WaitForSeconds(0.5f);

        RemoveParentCollider();
    }

    public void RemoveParentCollider()
    {
        if (gameObject.GetComponent<Collider>() != null)
            gameObject.GetComponent<Collider>().enabled = false;
    }

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
        if (isToBreak)
        {
            //check if collider exist to enable, else creates it
            if (myObj.GetComponent<Collider>() == null)
            {
                myObj.AddComponent<MeshCollider>();
                myObj.GetComponent<MeshCollider>().convex = true;
            }
            else
                myObj.GetComponent<Collider>().enabled = true;

            //check if rigidbody exist to disable kinematic, else creates it
            if (myObj.GetComponent<Rigidbody>() == null)
                myObj.AddComponent<Rigidbody>();
            else
                myObj.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}

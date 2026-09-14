using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MagicLibraryDesk : MagicFurniture
{
    [Header("MagicLibraryDesk Class")]
    [SerializeField] private float speedAbduction;
    [SerializeField] private GameObject destinationAbduction;
    [SerializeField] private GameObject TagsToAbduct;
    [SerializeField] private GameObject objectToAbduct;
    // bool isPause;

    void Update()
    {
        // if (!isPause)
        // {
        //     Debug.Log("Target Tag " + TagsToAbduct.tag);
        //     isPause = true;
        // }

        // if (GameObject.FindWithTag(TagsToAbduct.tag))
        // {
        //     Debug.Log("GO name: " + GameObject.FindWithTag(TagsToAbduct.tag).name + " tag: " + GameObject.FindWithTag(TagsToAbduct.tag).tag);
        // }
    }

    protected override IEnumerator ActivateAfterTime(float waitTime)
    {
        if (!objectToAbduct)
        {
            Debug.Log("abduction activate");
            yield return StartCoroutine(base.ActivateAfterTime(waitTime));

            FindBook();
            StartCoroutine(AbductBooks());
        }
    }

    /// <summary>
    /// find any object with the specific tag
    /// </summary>
    void FindBook()
    {
        if (GameObject.FindWithTag(TagsToAbduct.tag))
            objectToAbduct = GameObject.FindWithTag(TagsToAbduct.tag);
    }

    /// <summary>
    /// Move object target towards a point in scene, to simulate abduction
    /// </summary>
    /// <returns></returns>
    IEnumerator AbductBooks()
    {
        if (objectToAbduct)
        {
            DisableNavMeshComponent(objectToAbduct);

            Vector3 abductedPosition = objectToAbduct.transform.position;
            Vector3 finalPosition = destinationAbduction.transform.position;

            float distance;

            while (abductedPosition != finalPosition)
            {
                objectToAbduct.transform.position = Vector3.MoveTowards(objectToAbduct.transform.position, destinationAbduction.transform.position, 5 * Time.deltaTime);

                distance = Vector3.Distance(abductedPosition, finalPosition);

                abductedPosition = objectToAbduct.transform.position;
                finalPosition = destinationAbduction.transform.position;

                if (distance < 1.5f && distance > 0.5f)
                {
                    Vector3 decreaseSizeOverDistance = new Vector3(distance, distance, distance);
                    objectToAbduct.transform.localScale = decreaseSizeOverDistance;
                }

                yield return null;
            }

            FinishAbduction(objectToAbduct);
        }
    }

    /// <summary>
    /// Disable Nav Mesh Agent of a gameobject
    /// </summary>
    /// <param name="abductedObject"></param>
    void DisableNavMeshComponent(GameObject abductedObject)
    {
        if (abductedObject)
        {
            NavMeshAgent navMeshComponent;

            if (abductedObject.GetComponent<NavMeshAgent>())
            {
                navMeshComponent = abductedObject.GetComponent<NavMeshAgent>();
                navMeshComponent.enabled = false;
            }
        }
    }

    void FinishAbduction(GameObject abductedObject)
    {
        Destroy(abductedObject);
    }
}

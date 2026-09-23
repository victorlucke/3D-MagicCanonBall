using System.Collections;
using System.Security;
using UnityEngine;
using UnityEngine.AI;

public class MagicLibraryDesk : MagicFurniture
{
    [Header("MagicLibraryDesk Class")]
    [SerializeField] private float speedAbduction;
    [SerializeField] private GameObject destinationAbduction;
    [SerializeField] private GameObject tagsToAbduct;
    [SerializeField] private GameObject gateToOpen;
    private GameObject objectToAbduct;

    void Awake()
    {
        MagicAuraActivation("Magic", false);
    }

    void Update()
    {
        if (!isActivated && !objectToAbduct)
            FindBook();
        else if (!isActivated && objectToAbduct)
            MagicAuraActivation("Magic", true);

        if (isActivated)
            MagicAuraActivation("Magic", false);
    }

    protected override IEnumerator ActivateAfterTime(float waitTime)
    {
        Debug.Log("abduction activate");
        yield return base.ActivateAfterTime(waitTime);

        FindBook();
        StartCoroutine(AbductBooks());
    }

    /// <summary>
    /// find any object with the specific tag
    /// </summary>
    void FindBook()
    {
        if (GameObject.FindWithTag(tagsToAbduct.tag))
            objectToAbduct = GameObject.FindWithTag(tagsToAbduct.tag);
        else
            objectToAbduct = null;
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

    /// <summary>
    /// Destroy the abducted item
    /// </summary>
    /// <param name="abductedObject">object abducted reference</param>
    void FinishAbduction(GameObject abductedObject)
    {
        Destroy(abductedObject);
        GameEvents.TriggerOnOpenMagicGate(gateToOpen);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MagicAbductFurniture : MagicFurniture
{
    [Header("MagicAbductFurniture Class")]
    [SerializeField] private float speedAbduction;
    [Tooltip("Represent the position to be abducted")]
    [SerializeField] private GameObject destinationAbduction;
    [Tooltip("Represent Tag of abducted object")]
    [SerializeField] private GameObject tagsToAbduct;
    [Tooltip("Amount of objects to abduct before inactive (-1 or 0 = aways 1)")]
    [SerializeField] private int AmountToAbduct;
    [Tooltip("wich object will be destroyed after abduction (can be empty)")]
    [SerializeField] private GameObject gateToOpen;
    private int currentToAbducted;
    private GameObject objectToAbduct;

    protected override void Awake()
    {
        base.Awake();

        if (AmountToAbduct <= 0)
            currentToAbducted = 1;
        else currentToAbducted = AmountToAbduct;
    }

    protected override void Start()
    {
        base.Start();

        MagicAuraActivation(TriggerPrefab.tag, false);
    }

    void Update()
    {
        VerifyForAbduction();
    }

    private void VerifyForAbduction()
    {
        if (!isActivated && !objectToAbduct)
            FindToAbduct();
        else if (!isActivated && objectToAbduct)
            MagicAuraActivation(TriggerPrefab.tag, true);

        if (isActivated)
        {
            MagicAuraActivation(TriggerPrefab.tag, false);

            if (currentToAbducted > 0)
            {
                currentToAbducted--;
                isActivated = false;
            }
            else GameEvents.TriggerOnOpenMagicGate(gateToOpen);
        }
    }

    protected override IEnumerator ActivateAfterTime(float waitTime)
    {
        //Debug.Log("abduction activate");
        yield return base.ActivateAfterTime(waitTime);

        FindToAbduct();
        StartCoroutine(AbductBooks());
    }

    /// <summary>
    /// find any object with the specific tag
    /// </summary>
    void FindToAbduct()
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
                objectToAbduct.transform.position = Vector3.MoveTowards(objectToAbduct.transform.position, destinationAbduction.transform.position, speedAbduction * Time.deltaTime);

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

            DestroyAbducted();
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
    void DestroyAbducted()
    {
        Destroy(objectToAbduct);
    }
}

using System.Collections;
using UnityEngine;

public class RespawnChest : RespawnPoint
{
    public static int priorityChest;
    [Header("ChestSpecifics")]
    public GameObject chestCover;
    public float enablingTime;
    public float timeToOpenChest;
    public float maxAngleOpenChest;
    public float activateCost;
    [SerializeField] bool isRespawnEnabled;
    [SerializeField] bool isPlayerOnSpot;
    Coroutine enableRespawnCoroutine;
    float timeElapsed;
    float distanceOutAngle;
    int thisChestPriority;

    void Awake()
    {
        //isRespawnEnabled = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerOnSpot = true;
            if (!isRespawnEnabled)
            {
                Debug.Log(GameManager.Instance.magicAmount);
                if (GameManager.Instance.magicAmount >= activateCost)
                {
                    if (enableRespawnCoroutine == null)
                        enableRespawnCoroutine = StartCoroutine(EnableRespawnPoint(enablingTime));
                }
                else
                    Debug.Log("No Mana");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && enableRespawnCoroutine != null)
        {
            isPlayerOnSpot = false;
            StopCoroutine(enableRespawnCoroutine);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isRespawnEnabled && thisChestPriority == priorityChest)
            StartCoroutine(DelayRespawn(respawnTimer));
    }

    /// <summary>
    /// responsible for enabling respawn after a wait in place time is over
    /// </summary>
    /// <returns></returns>
    IEnumerator EnableRespawnPoint(float waitTime)
    {
        if (isPlayerOnSpot)
        {
            yield return new WaitForSeconds(waitTime);
            GameEvents.TriggerOnFillBar(-activateCost);
            StartCoroutine(OpenChest());
            isRespawnEnabled = true;
            priorityChest++;
            thisChestPriority = priorityChest;
        }
    }

    /// <summary>
    /// Responsible to rotate the chest cover open over time
    /// </summary>
    /// <returns></returns>
    IEnumerator OpenChest()
    {
        int i = 0;
        while (distanceOutAngle < maxAngleOpenChest)
        {
            timeElapsed += Time.deltaTime;
            distanceOutAngle = timeElapsed * (maxAngleOpenChest / timeToOpenChest); //Angulo desejado / tempo ate atingir o angulo (60 / 5 = 12; logo 5 x 12 = 60)

            if (timeElapsed > i + 1)
            {
                i++;
                Vector3 newRotation = new Vector3(-12, 0, 0);
                chestCover.transform.Rotate(newRotation);
            }
            //Debug.Log("Angle" + distanceOutAngle);
            yield return null;
        }
    }
}

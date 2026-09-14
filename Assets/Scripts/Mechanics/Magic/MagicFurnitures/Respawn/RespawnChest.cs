using System.Collections;
using UnityEngine;

public class RespawnChest : RespawnPoint
{
    protected static int priorityChest;
    [Header("RespawnChest Class")]
    public bool isRespawnEnabled;
    [SerializeField] private GameObject chestCover;
    [SerializeField] private float timeToOpenChest;
    [SerializeField] private float maxAngleOpenChest;
    private float timeElapsed;
    private float distanceOutAngle;
    private int thisChestPriority;

    void Awake()
    {
        //isRespawnEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated && thisChestPriority == priorityChest)
            StartCoroutine(DelayRespawn(respawnTimer));
    }

    /// <summary>
    /// responsible for enabling respawn after a wait in place time is over
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator ActivateAfterTime(float waitTime)
    {
        yield return StartCoroutine(base.ActivateAfterTime(waitTime));

        StartCoroutine(OpenChest());
        priorityChest++;
        thisChestPriority = priorityChest;
        yield return null;
    }

    /// <summary>
    /// Rotate the chest cover open over time
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
                Vector3 newRotation = new Vector3(-(maxAngleOpenChest / timeToOpenChest), 0, 0);
                chestCover.transform.Rotate(newRotation);
            }

            yield return null;
        }
    }
}

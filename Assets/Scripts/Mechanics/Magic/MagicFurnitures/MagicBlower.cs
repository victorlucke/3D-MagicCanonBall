using System.Collections;
using System.Drawing;
using UnityEngine;

public class MagicBlower : MagicFurniture
{
    [Header("Blower Class")]
    [SerializeField] private float blowSpeed;
    float sizeY;
    float sizeX;
    float sizeZ;

    void Awake()
    {
        sizeY = transform.localScale.y;
        sizeX = transform.localScale.x;
        sizeZ = transform.localScale.z;
    }

    protected override IEnumerator ActivateAfterTime(float activationDelay)
    {
        yield return base.ActivateAfterTime(activationDelay);

        yield return StartCoroutine(Blow());
    }

    IEnumerator Blow()
    {
        float currentSizeY = sizeY;
        float minSizeY = .5f;
        float maxSizeY = 1f;
        bool isDone = false;
        Debug.Log("Start Coroutine Blow");

        while (!isDone)
        {
            Debug.Log("Loop Blow");
            while (currentSizeY > minSizeY)
            {
                currentSizeY -= blowSpeed * Time.deltaTime;
                transform.localScale = new Vector3(sizeX, currentSizeY, sizeZ);
                yield return null;
            }

            while (currentSizeY < maxSizeY)
            {
                currentSizeY += blowSpeed * Time.deltaTime;
                transform.localScale = new Vector3(sizeX, currentSizeY, sizeZ);
                yield return null;
            }

            transform.localScale = new Vector3(sizeX, sizeY, sizeZ);

            isDone = true;
        }

        isActivated = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraEffect : BasicFunctionalities
{
    public float speed;
    public float speedRotation;
    public float dampingForce;
    public float auraDuration;
    private List<GameObject> insideAuraObjects = new List<GameObject>();

    void Awake()
    {
        speed = 2;
        speedRotation = 10;
        dampingForce = 5;
        auraDuration = 5;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayLoopSoundEffect();
        StartCoroutine(AuraLifeTime());
    }

    // Update is called once per frame
    void Update()
    {
        RotateAura();
        Move();
    }

    /// <summary>
    /// Follow players based on speed value
    /// </summary>
    void Move()
    {
        if (GameObject.FindWithTag("Player") != null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player").gameObject;


            Vector3 playerDirection = playerObject.transform.position - transform.position;

            transform.position = transform.position + playerDirection * speed * Time.deltaTime;
        }
    }

    /// <summary>
    /// Rotate the aura on Y.axi 10 degress per second with speedRotation value
    /// </summary>
    void RotateAura()
    {
        transform.Rotate(0, 10 * speedRotation * Time.deltaTime, 0);
    }


    /// <summary>
    /// Create a damping force on rigbodys that enter aura trigger
    /// </summary>
    /// <param name="invader">Object of the invader</param>
    /// <param name="apply">Should apply or remove slow effect</param>
    void AuraSlowEffect(GameObject invader, bool apply)
    {
        if (apply)
        {
            if (invader.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbody.linearDamping += dampingForce;
            }
        }
        else
        {
            if (invader.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbody.linearDamping -= dampingForce;
            }
        }
    }

    /// <summary>
    /// Save the newObject inside a list if he isnt there already
    /// </summary>
    /// <param name="newObject">GameObject</param>
    void SaveObjectInsideAura(GameObject newObject)
    {
        bool isSame = false;
        if (newObject.GetComponent<Rigidbody>() != null)
        {
            if (insideAuraObjects.Count > 0)
            {
                foreach (GameObject obj in insideAuraObjects)
                {
                    if (obj == newObject)
                        isSame = true;
                }

                if (!isSame) { }
                insideAuraObjects.Add(newObject);
            }
            else
            {
                insideAuraObjects.Add(newObject);
            }
        }
    }

    /// <summary>
    /// Delete the object inside the list if he exist
    /// </summary>
    /// <param name="newObject">GameObject</param>
    void DeleteObjectInsideAura(GameObject newObject)
    {
        bool isSame = false;

        foreach (GameObject obj in insideAuraObjects)
        {
            if (obj == newObject)
            {
                isSame = true;
            }
        }
        if (isSame)
        {
            insideAuraObjects.Remove(newObject);
        }
    }

    /// <summary>
    /// Garantee that every object inside Aura return to its normal damping
    /// </summary>
    void ResetSlowCondition()
    {
        foreach (GameObject obj in insideAuraObjects)
        {
            obj.GetComponent<Rigidbody>().linearDamping -= dampingForce;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        AuraSlowEffect(other.gameObject, true);

        SaveObjectInsideAura(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        AuraSlowEffect(other.gameObject, false);

        DeleteObjectInsideAura(other.gameObject);
    }

    /// <summary>
    /// Destroy Aura after duration time end
    /// </summary>
    /// <returns></returns>
    private IEnumerator AuraLifeTime()
    {

        yield return new WaitForSeconds(auraDuration);

        SummoningSpell spell = transform.parent.gameObject.GetComponent<SummoningSpell>();

        spell.auraFinished = true;
        spell.spawnPosition = transform.position;

        ResetSlowCondition();

        Destroy(this.gameObject);
    }
}

using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MagicFurniture : MonoBehaviour
{
    [Header("MagicFurniture Class")]
    [Tooltip("time to wait before activating magic effect (Can be empty)")]
    [SerializeField] protected int activateCost;
    [Tooltip("time to wait before activating magic effect (Can be empty)")]
    [SerializeField] protected float activationDelay;
    [Tooltip("the target allowed to activate the magic furniture")]
    [SerializeField] protected GameObject TargetPrefab;
    [Tooltip("prefab of the aura (trigger) used to activate effect")]
    [SerializeField] protected GameObject TriggerPrefab;
    protected GameObject myMagicAura;
    [SerializeField] protected bool isTargetOnSpot;
    [SerializeField] protected bool isActivated;
    protected Coroutine coroutineOnceActivateAfterTime;
    protected Coroutine coroutineOnceCheckForMagicAura;
    protected Coroutine coroutineOnceWaitToReactivateFurniture;

    protected virtual void Awake()
    {
        if (!GetComponent<Rigidbody>())
            Debug.Log("Needs a RigdyBody in " + gameObject.name);
    }

    protected virtual void Start()
    {
        CheckForMagicAura(TriggerPrefab.tag);
    }

    /// <summary>
    /// Verify if target is on spot
    /// </summary>
    /// <param name="other"></param>
    protected virtual void OnTriggerStay(Collider other)
    {
        if (TargetPrefab.CompareTag(other.tag))
        {
            if (!isActivated)
            {
                isTargetOnSpot = true;

                if (GameManager.Instance.magicAmount >= activateCost)
                {
                    if (coroutineOnceActivateAfterTime == null)
                        coroutineOnceActivateAfterTime = StartCoroutine(ActivateAfterTime(activationDelay));
                }
                else
                    Debug.Log("No Mana");
            }
        }
        // else
        // {
        //     isTargetOnSpot = false;
        //     if (coroutineOnceActivateAfterTime != null)
        //         StopCoroutine(coroutineOnceActivateAfterTime);
        // }
    }

    /// <summary>
    /// Verify if target leave the spot
    /// </summary>
    /// <param name="other"></param>
    protected virtual void OnTriggerExit(Collider other)
    {
        if (TargetPrefab.CompareTag(other.tag))
        {
            isTargetOnSpot = false;
            if (coroutineOnceActivateAfterTime != null)
            {
                Debug.Log("Stop Coroutine base");
                StopCoroutine(coroutineOnceActivateAfterTime);
                coroutineOnceActivateAfterTime = null;
            }
        }
    }

    /// <summary>
    /// Spend mana after wait time is over to activate
    /// </summary>
    /// <param name="activationDelay"></param>
    /// <returns></returns>
    protected virtual IEnumerator ActivateAfterTime(float activationDelay)
    {
        if (isTargetOnSpot)
        {
            //Debug.Log("Starting coroutine base");
            yield return new WaitForSeconds(activationDelay);
            isActivated = true;
            GameEvents.TriggerOnFillBar(-activateCost);
            //Debug.Log("finished coroutine base");
        }
    }

    /// <summary>
    /// Verify if there is an MagicAura as child and return her game object
    /// </summary>
    /// <param name="auraTag">the tag of the magic aura to return</param>
    /// <returns></returns>
    protected virtual IEnumerator CheckForMagicAura(String auraTag)
    {
        int objectsSearchedThisFrame = 0;
        int searchLimitPerFrame = 500;

        foreach (Transform child in transform)
        {
            if (child.CompareTag(auraTag))
            {
                myMagicAura = child.gameObject;
                break;
            }

            objectsSearchedThisFrame++;

            if (objectsSearchedThisFrame >= searchLimitPerFrame)
            {
                objectsSearchedThisFrame = 0;
                yield return null;
            }
        }

        if (!myMagicAura)
            Debug.Log("No Magic Aura Attached on " + gameObject.name);

        yield return null;
    }

    /// <summary>
    /// Switch to activate the magic aura of furniture. 
    /// (Magic aura is used to activate magic effect of the furniture when in contact with player or other object)
    /// </summary>
    /// <param name="auraTag">the tag of magic aura</param>
    /// <param name="activation">set the aura Active</param>
    protected virtual void MagicAuraActivation(String auraTag, bool activation)
    {
        if (coroutineOnceCheckForMagicAura == null)
            coroutineOnceCheckForMagicAura = StartCoroutine(CheckForMagicAura(auraTag));

        if (myMagicAura)
        {
            myMagicAura.SetActive(activation);
            myMagicAura = null;
            coroutineOnceCheckForMagicAura = null;
        }
    }

    /// <summary>
    /// enabled the use of the furniture spell again even if it was already used before. if time > 0
    /// </summary>
    /// <param name="timeToWait">After how much time is to reactivate</param>
    protected virtual void ReactivateFurniture(int timeToWait)
    {
        if (coroutineOnceWaitToReactivateFurniture == null && timeToWait > 0)
            coroutineOnceWaitToReactivateFurniture = StartCoroutine(WaitToReactivateFurniture(timeToWait));
    }

    /// <summary>
    /// Wait for seconds before enable the use of furniture spell again
    /// </summary>
    /// <param name="timeToWait">amount of time to wait</param>
    /// <returns></returns>
    protected virtual IEnumerator WaitToReactivateFurniture(int timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);

        isActivated = false;
        coroutineOnceWaitToReactivateFurniture = null;
    }
}

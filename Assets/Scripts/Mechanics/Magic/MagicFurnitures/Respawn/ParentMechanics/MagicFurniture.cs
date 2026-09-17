using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class MagicFurniture : MonoBehaviour
{
    [Header("MagicFurniture Class")]

    /// <summary>
    /// the target allowed to activate the magic fiture
    /// </summary>
    [SerializeField] protected GameObject targetPrefab;
    [SerializeField] protected float waitTime;
    [SerializeField] protected int activateCost;
    [SerializeField] protected bool isTargetOnSpot;
    protected GameObject myMagicAura;
    protected bool isActivated;
    private Coroutine startCoroutineOnce01;
    private Coroutine startCoroutineOnce02;
    private Coroutine startCoroutineOnce03;

    /// <summary>
    /// Verify if target is on spot
    /// </summary>
    /// <param name="other"></param>
    protected virtual void OnTriggerStay(Collider other)
    {
        if (targetPrefab.CompareTag(other.tag))
        {
            if (!isActivated)
            {
                isTargetOnSpot = true;

                if (GameManager.Instance.magicAmount >= activateCost)
                {
                    if (startCoroutineOnce01 == null)
                        startCoroutineOnce01 = StartCoroutine(ActivateAfterTime(waitTime));
                }
                else
                    Debug.Log("No Mana");
            }

        }
        // else
        // {
        //     isTargetOnSpot = false;
        //     if (startCoroutineOnce01 != null)
        //         StopCoroutine(startCoroutineOnce01);
        // }
    }

    /// <summary>
    /// Verify if target leave the spot
    /// </summary>
    /// <param name="other"></param>
    protected virtual void OnTriggerExit(Collider other)
    {
        if (targetPrefab.CompareTag(other.tag))
        {
            isTargetOnSpot = false;
            if (startCoroutineOnce01 != null)
            {
                StopCoroutine(startCoroutineOnce01);
                startCoroutineOnce01 = null;
            }
        }
    }

    /// <summary>
    /// Spend mana after wait time is over
    /// </summary>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    protected virtual IEnumerator ActivateAfterTime(float waitTime)
    {
        if (isTargetOnSpot)
        {
            Debug.Log("Starting coroutine");
            yield return new WaitForSeconds(waitTime);
            GameEvents.TriggerOnFillBar(-activateCost);
            isActivated = true;
            startCoroutineOnce01 = null;
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
            if (child.CompareTag("Magic"))
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
        if (startCoroutineOnce02 == null)
            startCoroutineOnce02 = StartCoroutine(CheckForMagicAura(auraTag));

        if (myMagicAura)
        {
            myMagicAura.SetActive(activation);
            myMagicAura = null;
            startCoroutineOnce02 = null;
        }
    }

    /// <summary>
    /// enabled the use of the furniture spell again even if it was already used before.
    /// </summary>
    /// <param name="timeToWait">After how much time is to reactivate</param>
    protected virtual void ReactivateFurniture(int timeToWait)
    {
        if (startCoroutineOnce03 == null)
            startCoroutineOnce03 = StartCoroutine(WaitToReactivateFurniture(timeToWait));
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
        startCoroutineOnce03 = null;
    }
}

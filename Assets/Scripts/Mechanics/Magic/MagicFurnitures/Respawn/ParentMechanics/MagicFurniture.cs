using System.Collections;
using UnityEngine;

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
    protected Coroutine startCoroutineOnce;
    protected bool isActivated;

    /// <summary>
    /// Verify if target is on spot
    /// </summary>
    /// <param name="other"></param>
    protected virtual void OnTriggerStay(Collider other)
    {
        if (targetPrefab.CompareTag(other.tag))
        {
            isTargetOnSpot = true;
            if (!isActivated)
            {
                if (GameManager.Instance.magicAmount >= activateCost)
                {
                    if (startCoroutineOnce == null)
                        startCoroutineOnce = StartCoroutine(ActivateAfterTime(waitTime));
                }
                else
                    Debug.Log("No Mana");
            }
        }
        // else
        // {
        //     isTargetOnSpot = false;
        //     if (startCoroutineOnce != null)
        //         StopCoroutine(startCoroutineOnce);
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
            if (startCoroutineOnce != null)
                StopCoroutine(startCoroutineOnce);
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
            yield return new WaitForSeconds(waitTime);
            GameEvents.TriggerOnFillBar(-activateCost);
            isActivated = true;
        }
    }
}

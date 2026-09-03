using System.Collections;
using System.Linq;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [Header("ParentClass")]
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected GameObject RespawnReference;
    [SerializeField] protected int remainingNumberToRespawn;
    [SerializeField] protected float respawnTimer;
    protected GameObject respawnReference;

    protected void Respawn()
    {
        if (VerifyIsToRespawn())
            respawnReference = Instantiate(prefab, RespawnReference.transform.position, prefab.transform.rotation);
    }

    protected IEnumerator DelayRespawn(float waitTime)
    {
        if (VerifyIsToRespawn())
        {
            yield return new WaitForSeconds(waitTime);

            Respawn();
        }
    }

    bool VerifyIsToRespawn()
    {
        int prefabsInScene = GameObject.FindGameObjectsWithTag(prefab.tag).Length;

        if (prefabsInScene == remainingNumberToRespawn)
            return true;

        else return false;
    }
}

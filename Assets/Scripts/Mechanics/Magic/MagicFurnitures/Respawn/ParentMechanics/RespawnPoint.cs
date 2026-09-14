using System.Collections;
using System.Linq;
using UnityEngine;

public abstract class RespawnPoint : MagicFurniture
{
    [Header("RespawnPoint")]
    [SerializeField] protected GameObject prefabToRespawn;
    [SerializeField] protected GameObject RespawnLocation;
    [SerializeField] protected int remainingNumberToRespawn;
    [SerializeField] protected float respawnTimer;
    protected GameObject respawnReference;

    protected void Respawn()
    {
        if (VerifyIsToRespawn())
            respawnReference = Instantiate(prefabToRespawn, RespawnLocation.transform.position, prefabToRespawn.transform.rotation);
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
        int prefabsInScene = GameObject.FindGameObjectsWithTag(prefabToRespawn.tag).Length;

        if (prefabsInScene == remainingNumberToRespawn)
            return true;

        else return false;
    }
}

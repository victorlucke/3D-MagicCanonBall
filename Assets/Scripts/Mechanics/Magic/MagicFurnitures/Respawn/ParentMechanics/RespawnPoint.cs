using System.Collections;
using System.Linq;
using UnityEngine;

public abstract class RespawnPoint : MagicFurniture
{
    [Header("RespawnPoint")]
    [SerializeField] protected GameObject prefabToRespawn;
    [SerializeField] protected GameObject respawnLocation;
    [SerializeField] protected int remainingNumberToRespawn;
    [SerializeField] protected float respawnTimer;
    protected GameObject respawnReference;

    /// <summary>
    /// respawn the prefabToRespawn Object in respawnLocation
    /// </summary>
    protected void Respawn()
    {
        if (VerifyIsToRespawn())
            respawnReference = Instantiate(prefabToRespawn, respawnLocation.transform.position, prefabToRespawn.transform.rotation);
    }

    /// <summary>
    /// wait some time before iniciate respawn
    /// </summary>
    /// <param name="waitTime">time to wait</param>
    /// <returns></returns>
    protected IEnumerator DelayRespawn(float waitTime)
    {
        if (VerifyIsToRespawn())
        {
            yield return new WaitForSeconds(waitTime);

            Respawn();
        }
    }

    /// <summary>
    /// Verify if the amount of objects remaining in scene is enouth to allow respawning it
    /// </summary>
    /// <returns></returns>
    bool VerifyIsToRespawn()
    {
        int prefabsInScene = GameObject.FindGameObjectsWithTag(prefabToRespawn.tag).Length;

        if (prefabsInScene == remainingNumberToRespawn)
            return true;

        else return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FisicalEffectsManager : MonoBehaviour
{
    public static FisicalEffectsManager Instance;
    public enum AvaibleEffects { Wind, Blind }
    public Dictionary<GameObject, AvaibleEffects> AffectedObjects { get { return _affectedObjects; } }
    public float effectStandardDuration { get { return _effectStandardDuration; } }
    private static Dictionary<GameObject, AvaibleEffects> _affectedObjects = new Dictionary<GameObject, AvaibleEffects>();
    private static Dictionary<GameObject, Coroutine> beingAffected = new Dictionary<GameObject, Coroutine>();
    private static Dictionary<AvaibleEffects, float> effectsDurations = new Dictionary<AvaibleEffects, float>();
    private float _effectStandardDuration;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else Destroy(gameObject);

        _effectStandardDuration = 10;
    }

    void OnEnable()
    {
        GameEvents.OnFisicalEffect += AddNewObjectAffected;
    }

    void OnDisable()
    {
        GameEvents.OnFisicalEffect -= AddNewObjectAffected;
    }

    /// <summary>
    /// add an object and the fisical effect to apply in it every time the action event is called
    /// </summary>
    /// <param name="myObject">object affected</param>
    /// <param name="myEffect">the effects to apply</param>
    void AddNewObjectAffected(GameObject myObject, AvaibleEffects myEffect)
    {
        if (!_affectedObjects.ContainsKey(myObject))
        {
            //Debug.Log("Adding object to List of effected " + myObject.name);
            _affectedObjects.Add(myObject, myEffect);
        }
        DisableEffectManagement(myObject);
    }   

    /// <summary>
    /// Save every object being affected to start or stop deactivation process
    /// </summary>
    /// <param name="myObject">object being affected</param>
    void DisableEffectManagement(GameObject myObject)
    {
        if (!beingAffected.ContainsKey(myObject))
            beingAffected.Add(myObject, StartCoroutine(DisableEffect(myObject)));
        else
        {
            StopCoroutine(beingAffected[myObject]);
            beingAffected[myObject] = StartCoroutine(DisableEffect(myObject));
        }
    }

    /// <summary>
    /// deactivate the effect in object after duration time
    /// </summary>
    /// <param name="targetOfEffect">object being affected</param>
    /// <returns></returns>
    IEnumerator DisableEffect(GameObject targetOfEffect)
    {
        yield return new WaitForSeconds(_effectStandardDuration);
        beingAffected.Remove(targetOfEffect);
        _affectedObjects.Remove(targetOfEffect);
    }
}

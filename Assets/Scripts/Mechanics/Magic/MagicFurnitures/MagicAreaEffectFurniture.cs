using System.Collections;
using UnityEngine;

public class MagicAreaEffectFurniture : MagicFurniture
{
    [Header("AreaEffectFurniture Class")]
    private DetectionArea detectionArea;
    [Tooltip("the Target affected by fisical effects")]
    [SerializeField] private GameObject prefabTargetToEffect;
    [Tooltip("The object to area of effect being attacheched too (Recommended to be outside the obj attacheched to this script for trigger detection problens)")]
    [SerializeField] private GameObject pivotAreEffect;
    [Tooltip("the range to the area of effect")]
    [SerializeField] private float rangeOfDetection; 
    [SerializeField] private FisicalEffectsManager.AvaibleEffects effectToApply;

    protected override void Awake()
    {
        base.Awake();
        detectionArea = new DetectionArea().CreateDetectionArea(pivotAreEffect.transform, rangeOfDetection, prefabTargetToEffect);
    }

    void Update()
    {
        //ApplyEffectOnTarget();
    }

    protected override IEnumerator ActivateAfterTime(float activationDelay)
    {
        yield return base.ActivateAfterTime(activationDelay);
        ApplyEffectOnTarget();
    }

    void ApplyEffectOnTarget()
    {
        if (detectionArea.objectTargetDetected && detectionArea.isInRange)
        {
            Debug.Log("adding effect object" + detectionArea.objectTargetDetected.name);
            GameEvents.TriggerOnFisicalEffect(detectionArea.objectTargetDetected, effectToApply);
        }
    }
}

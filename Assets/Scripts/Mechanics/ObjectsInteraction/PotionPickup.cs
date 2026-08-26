using UnityEngine;

public class PotionPickup : CollectObject
{
    public float potionValue;
    protected override float IncrementValue()
    {
        GameEvents.TriggerOnFillBar(potionValue);
        return 10;
    }
}

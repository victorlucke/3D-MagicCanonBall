using UnityEngine;

public class PotionPickup : CollectObject
{
    public float potionValue;
    protected override float IncrementValue()
    {
        GameManager.Instance.manaAmount += potionValue;
        GameEvents.TriggerOnFillBar(potionValue);
        return potionValue;
    }
}

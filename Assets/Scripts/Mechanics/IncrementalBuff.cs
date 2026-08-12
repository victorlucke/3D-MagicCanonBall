using UnityEngine;

public class IncrementalBuff : CollectObject
{
    protected override float IncrementValue()
    {
        Debug.Log("incrementando valor");
        return 10;
    }
}

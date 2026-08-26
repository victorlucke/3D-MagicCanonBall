using UnityEngine;

public class FloorLava : Floors
{
    public float strengthSink;
    void Awake()
    {
        thisFloorType = FloorType.Sand;
        strengthSink = 5;
    }
    protected override void FloorCondition()
    {
        if(playerObject != null)
        {
            GameManager.Instance.GameOver();
        }
    }
}

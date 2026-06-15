using FirstGearGames.SmoothCameraShaker;
using UnityEngine;

public class FloorStone : Floors
{
    public ShakeData floorStoneShake;
    protected override void FloorCondition()
    {
        CameraShakerHandler.Shake(floorStoneShake);
    }
}

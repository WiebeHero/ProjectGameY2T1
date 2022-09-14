using System.Collections;
using System.Collections.Generic;
using TerrainMovement;
using UnityEngine;

public class GasPedal : Interactable.Interactable
{
    
    protected override void OnLeftClick()
    {
        
    }
    
    protected override void OnLeftClickHold()
    {
        if (MovingTerrainManager.speed > 1.0F)
        {
            MovingTerrainManager.speed = MovingTerrainManager.speed - 0.15F;
        }
    }
}

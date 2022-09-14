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
        if (MovingTerrainManager.Speed > 1.0F)
        {
            MovingTerrainManager.Speed = MovingTerrainManager.Speed - 0.15F;
        }
    }
}

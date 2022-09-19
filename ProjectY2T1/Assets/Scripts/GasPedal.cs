using System;
using System.Collections;
using System.Collections.Generic;
using TerrainMovement;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;

public class GasPedal : Interactable
{

    [SerializeField] private GameObject foot;
    
    void Start()
    {
        if (foot == null) throw new Exception("Foot is not assigned!");
        Color color = foot.GetComponent<Renderer>().material.color;
        color.a = 1.0F;
    }
    
    protected override void OnLookingAt()
    {
        
    }
    
    protected override void OnStopLookingAt()
    {
        Debug.Log("Stopped looking");
    }
    
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

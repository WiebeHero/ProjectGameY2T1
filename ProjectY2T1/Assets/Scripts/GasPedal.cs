using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasPedal : Interactable.Interactable
{
    [SerializeField] private Car car;
    
    protected override void OnLeftClick()
    {
        
    }
    
    protected override void OnLeftClickHold()
    {
        if (car.speed > 1.0F)
        {
            car.speed -= 0.15F;
        }
    }
}

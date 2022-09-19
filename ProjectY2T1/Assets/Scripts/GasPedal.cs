using System;
using System.Collections;
using System.Collections.Generic;
using TerrainMovement;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;

public class GasPedal : Interactable
{

    [SerializeField] private GameObject foot;
    [SerializeField] private Animator animator;

    private Material mat;
    private Color color;
    private bool looking;
    private static readonly int Activate = Animator.StringToHash("Activate");

    void Start()
    {
        if (foot == null) throw new Exception("Foot is not assigned!");
        mat = foot.GetComponent<Renderer>().material;
        color = mat.color;
    }

    private void FixedUpdate()
    {
        if (looking)
        {
            foot.SetActive(true);
            if (color.a <= 0.98F) color.a += 0.02F;
        }
        else
        {
            if(foot.activeSelf)
                if (color.a >= 0.02F)
                    color.a -= 0.02F;
                else
                    foot.SetActive(false);
        }
        mat.color = color;
    }

    protected override void OnLookingAt()
    {
        looking = true;
    }
    
    protected override void OnStopLookingAt()
    {
        looking = false;
    }
    
    protected override void OnLeftClick()
    {
        animator.SetTrigger(Activate);
    }
    
    protected override void OnLeftClickHold()
    {
        if (MovingTerrainManager.speed > 2.0F)
        {
            MovingTerrainManager.speed -= 0.15F;
        }
    }

    protected override void OnLeftClickRelease()
    {
        animator.Play("GasPedalRetreat", -1, float.NegativeInfinity);
    }
}

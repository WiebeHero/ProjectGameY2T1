using System;
using UnityEngine;

public class Tape : Interactable
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private AnimationClip clipAnimation;
    [SerializeField] private CassettePlayer cassettePlayer;
    [SerializeField] private CassettePlayer.Tapes tapeType;
    private static readonly int Tapes = Animator.StringToHash("Tapes");

    private void Start()
    {
        if (cassettePlayer == null) 
            throw new Exception($"No Cassette Player assigned to tape");
    }

    protected override void OnLookingAt()
    {
        //Debug.Log("Deez");
    }

    protected override void OnStopLookingAt()
    {
        
    }

    protected override void OnLeftClick()
    {
        //Debug.Log("Deez2");
        //cassettePlayer.PlayTape(this);
        //gameObject.SetActive(false);
        cassettePlayer.PlayTape(this);
    }

    protected override void OnLeftClickHold() {}
    
    protected override void OnLeftClickRelease() {}

    public AudioClip Clip
    {
        get => clip;
    }
    public AnimationClip AnimationClip
    {
        get => clipAnimation;
    }
    

    public CassettePlayer.Tapes TapeType
    {
        get => tapeType;
    }
}
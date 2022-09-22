using System;
using UnityEngine;

public class Tape : Interactable
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private CassettePlayer cassettePlayer;
    [SerializeField] private CassettePlayer.Tapes tapeType;
    private static readonly int Tapes = Animator.StringToHash("Tapes");

    private void Start()
    {
        if (cassettePlayer == null) 
            throw new Exception($"No Cassette Player assigned to tape");

        if (audioSource == null)
            throw new Exception("Tape has no AudioSource");
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
        cassettePlayer.Animator.SetInteger(Tapes, (int)tapeType);
    }

    protected override void OnLeftClickHold() {}
    
    protected override void OnLeftClickRelease() {}

    public AudioSource AudioSource
    {
        get => audioSource;
        set => audioSource = value;
    }
}
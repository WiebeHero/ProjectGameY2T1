using UnityEngine;
// ReSharper disable InconsistentNaming

public class Tape : Interactable
{
    [SerializeField] private AudioClip clip_;
    [SerializeField] private AnimationClip clipAnimation;
    [SerializeField] private CassettePlayer cassettePlayer;
    [SerializeField] private CassettePlayer.Tapes tapeType_;

    private void Start()
    {
        if (cassettePlayer == null) 
            Debug.LogWarning($"No Cassette Player assigned to tape");
    }

    protected override void OnLookingAt(){}

    protected override void OnStopLookingAt(){}

    protected override void OnLeftClick() => cassettePlayer.PlayTape(this);

    protected override void OnLeftClickHold(){}
    
    protected override void OnLeftClickRelease(){}

    public AudioClip clip => clip_;
    public AnimationClip animationClip => clipAnimation;
    public CassettePlayer.Tapes tapeType => tapeType_;
}
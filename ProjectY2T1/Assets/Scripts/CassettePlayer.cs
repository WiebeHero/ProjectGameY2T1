using System;
using UnityEngine;

public class CassettePlayer : MonoBehaviour
{
    public event Action LastTapeStoppedPlayingEvent;
    [SerializeField] private Animator tapeAnimator;

    private bool tapeIsPlaying;
    private Tape currentlyPlaying;

    public enum Tapes
    {
        Ex1 = 1,
        Ex2 = 2,
        Ex3 = 3,
        Ex4 = 4,
        Ex5 = 5,
        Ex6 = 6
    }

    private void FixedUpdate()
    {
        if (!tapeIsPlaying) return;
        if (!currentlyPlaying.AudioSource.isPlaying)
        {
            currentlyPlaying.gameObject.SetActive(true);
            LastTapeStoppedPlayingEvent?.Invoke();
            currentlyPlaying = null;
        }
    }

    public void PlayTape(Tape tape)
    {
        currentlyPlaying = tape;

        currentlyPlaying.AudioSource.Play();

    }

    public Animator Animator
    {
        get => tapeAnimator;
    }
}
using System;
using UnityEngine;

public class CassettePlayer : MonoBehaviour
{
    public event Action LastTapeStoppedPlayingEvent;

    private bool tapeIsPlaying;
    private Tape currentlyPlaying;

    public enum Tapes
    {
        Ex1,
        Ex2,
        Ex3
    }

    private void FixedUpdate()
    {
        if (!tapeIsPlaying) return;
        if (!currentlyPlaying.audioSource.isPlaying)
        {
            currentlyPlaying.gameObject.SetActive(true);
            LastTapeStoppedPlayingEvent?.Invoke();
            currentlyPlaying = null;
        }
    }

    public void PlayTape(Tape tape)
    {
        currentlyPlaying = tape;

        currentlyPlaying.audioSource.Play();

    }
}

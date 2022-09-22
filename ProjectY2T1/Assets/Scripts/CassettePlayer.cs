using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class CassettePlayer : MonoBehaviour
{
    public event Action LastTapeStoppedPlayingEvent;
    [SerializeField] private Animator tapeAnimator;

    private AudioSource source;
    private bool tapeIsPlaying;
    private Tape currentlyPlaying;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        if (source == null) throw new Exception("No Audio Source detected on Cassete Player!");
    }

    public enum Tapes
    {
        Ex1 = 1,
        Ex2 = 2,
        Ex3 = 3,
        Ex4 = 4,
        Ex5 = 5,
        Ex6 = 6
    }

    private void Update()
    {
        if(InformationManager.paused || InformationManager.isCrashing) source.Pause();
        else source.UnPause();
    }

    private void FixedUpdate()
    {
        /*if (!tapeIsPlaying) return;
        if (!currentlyPlaying.AudioSource.isPlaying)
        {
            currentlyPlaying.gameObject.SetActive(true);
            LastTapeStoppedPlayingEvent?.Invoke();
            currentlyPlaying = null;
        }*/
    }

    public void PlayTape(Tape tape)
    {
        if(currentlyPlaying != null) tapeAnimator.SetInteger("Tapes", 0);
        currentlyPlaying = tape;
        source.Stop();
        StartCoroutine(PlayTapeAfter(tape.AnimationClip.length));
    }

    IEnumerator PlayTapeAfter(float length)
    {
        yield return new WaitForSeconds(length);
        if(currentlyPlaying != null) tapeAnimator.SetInteger("Tapes", (int)currentlyPlaying.TapeType);

        source.clip = currentlyPlaying.Clip;

        source.Play();
    }

    public Animator Animator
    {
        get => tapeAnimator;
    }
}
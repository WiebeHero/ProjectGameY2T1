using System.Collections;
using Managers;
using UnityEngine;

public class CassettePlayer : MonoBehaviour
{
    [SerializeField] private Animator tapeAnimator;

    private AudioSource source;
    private bool tapeIsPlaying;
    private Tape currentlyPlaying;
    private static readonly int Tapes1 = Animator.StringToHash("Tapes");

    private void Start()
    {
        source = GetComponent<AudioSource>();
        if (source == null) Debug.LogWarning("No Audio Source detected on Cassete Player!");
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

    public void PlayTape(Tape tape)
    {
        if(currentlyPlaying != null) tapeAnimator.SetInteger(Tapes1, 0);
        currentlyPlaying = tape;
        source.Stop();
        StartCoroutine(PlayTapeAfter(tape.animationClip.length));
    }

    private IEnumerator PlayTapeAfter(float length)
    {
        yield return new WaitForSeconds(length);
        if(currentlyPlaying != null) tapeAnimator.SetInteger(Tapes1, (int)currentlyPlaying.tapeType);

        source.clip = currentlyPlaying.clip;

        source.Play();
    }

    public Animator animator => tapeAnimator;
}
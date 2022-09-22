using System;
using System.Collections;
using Managers;
using UnityEngine;

public sealed class CrashSequenceEnd : MonoBehaviour
{
    [SerializeField] private AudioSource screechSource, crashSource, bumpSource;

    private void Start()
    {
        if (screechSource == null || crashSource == null || bumpSource == null) 
            throw new Exception("AudioSource component not present.");
    }
    public IEnumerator HoldUpWaitAMinute()
    {
        yield return new WaitForSeconds(2);
        EventHub.TriggerEvent(EventHub.CustomEvent.CrashStopEvent);
    }

    public void PlayScreech() => screechSource.Play();

    public void PlayCrash() => crashSource.Play();

    public void PlayBump() => bumpSource.Play();
}

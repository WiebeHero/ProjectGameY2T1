using System;
using System.Collections;
using Managers;
using UnityEngine;

public class CrashSequenceEnd : MonoBehaviour
{
    [SerializeField] private AudioSource screechSource, crashSource, bumpSource;

    void Start()
    {
        if (screechSource == null || crashSource == null || bumpSource == null) throw new Exception("AudioSource component not present.");
    }
    public IEnumerator HoldUpWaitAMinute()
    {
        yield return new WaitForSeconds(2);
        EventHub.TriggerEvent(EventHub.CustomEvent.CrashStopEvent);
    }

    public void PlayScreech()
    {
        screechSource.Play();
        Debug.Log("Played1!");
        Debug.Log("Hihi");
    }
    
    public void PlayCrash()
    {
        crashSource.Play();
        Debug.Log("Played2!");
    }
    
    public void PlayBump()
    {
        bumpSource.Play();
        Debug.Log("Played3!");
    }
}

using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class CrashSequenceEnd : MonoBehaviour
{
    public void CrashSequenceEnding()
    {
        EventHub.TriggerEvent(EventHub.CustomEvent.CrashStopEvent);
    }
}

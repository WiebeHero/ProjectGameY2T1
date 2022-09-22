using System.Collections;
using Managers;
using UnityEngine;

public class CrashSequenceEnd : MonoBehaviour
{
    public IEnumerator HoldUpWaitAMinute()
    {
        yield return new WaitForSeconds(2);
        EventHub.TriggerEvent(EventHub.CustomEvent.CrashStopEvent);
    }
}

using Managers;
using UnityEngine;

public class CarSound : MonoBehaviour
{
    private AudioSource source;
    private void Start() => source = GetComponent<AudioSource>();
    private void Update()
    {
        if (InformationManager.paused || InformationManager.isCrashing) source.Pause();
        else source.UnPause();
    }
}

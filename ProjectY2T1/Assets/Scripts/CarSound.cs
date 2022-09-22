using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class CarSound : MonoBehaviour
{

    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InformationManager.paused || InformationManager.isCrashing) source.Pause();
        else source.UnPause();
    }
}

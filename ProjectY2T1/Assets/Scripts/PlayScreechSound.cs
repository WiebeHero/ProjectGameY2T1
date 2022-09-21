using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScreechSound : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    private bool played;
    
    public void PlayScreech()
    {
        if (!played)
        {
            source.Play();
            played = true;
        }
    }
}

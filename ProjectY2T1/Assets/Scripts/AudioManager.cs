﻿using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager i;

    // [Header("Car noises")]   
    // [SerializeField]
    // public AudioSource carNoise;

    
    
    
    private AudioSource audioSource;


    private void Awake()
    {
        if (i == null) {
            i = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
}

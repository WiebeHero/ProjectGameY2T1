using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class WindShieldTrigger : MonoBehaviour
{
    [SerializeField] private GameObject window;
    [SerializeField] private List<GameObject> watchOutFor;
    private AudioSource source;
    private bool played;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        if (source == null) throw new Exception("There is no Audio Source component!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (watchOutFor.Contains(other.gameObject))
        {
            window.SetActive(true);
            
            if (!played)
            {
                played = true;
                source.Play();
            }
        }
    }
}

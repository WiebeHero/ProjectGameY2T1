using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindShieldTrigger : MonoBehaviour
{
    [SerializeField] private GameObject window;
    [SerializeField] private List<GameObject> watchOutFor;

    private void OnTriggerEnter(Collider other)
    {
        if (watchOutFor.Contains(other.gameObject))
        {
            window.SetActive(true);
        }
    }
}

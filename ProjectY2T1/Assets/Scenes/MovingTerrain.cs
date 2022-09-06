using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTerrain : MonoBehaviour
{
    [SerializeField]
    private Vector3 moveDirection;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += moveDirection;

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Test");
    }
}

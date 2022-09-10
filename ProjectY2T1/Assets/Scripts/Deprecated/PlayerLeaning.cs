using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeaning : MonoBehaviour
{
    Transform cam;
    public Transform camOffset;
    float rotX;
    float rotY;

    [Header("Factors")]
    public float RotationSpeed = 2;
    public float LeanFactor = 1;

    [Header("Clamp Values")]
    public Vector2 ClampX;
    public Vector2 ClampY;

    // Start is called before the first frame update
    void Start()
    {
        cam = transform.GetComponentInChildren<Camera>().gameObject.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Get inputs
        rotX += Input.GetAxis("Mouse X")*RotationSpeed;
        rotY += Input.GetAxis ("Mouse Y")*RotationSpeed;

        //Clamp Rotations
        rotX = Mathf.Clamp(rotX, ClampX.x, ClampX.y);
        rotY = Mathf.Clamp(rotY, ClampY.x, ClampY.y);

        //Rotate cam and empty
        cam.transform.localRotation = Quaternion.Euler(-rotY,rotX,0f);
        camOffset.transform.localRotation = Quaternion.Euler(0f,0f,rotX*.1f*LeanFactor);
        transform.localRotation = Quaternion.Euler(0f,0f,-rotX*.1f*LeanFactor);
    }
}

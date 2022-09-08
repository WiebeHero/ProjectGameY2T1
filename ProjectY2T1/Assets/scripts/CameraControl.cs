using System;
using Managers;
using UnityEngine;
using Cursor = UnityEngine.Cursor;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform camOffset;

    [SerializeField] private float fov = 60f;
    
    [Header("Factors")]
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private float leanFactor = 2;
    
    [Header("Clamp Values")]
    [SerializeField] private Vector2 clampX;
    [SerializeField] private Vector2 clampY;


    [Header("Interaction")]
    [SerializeField] private float interactionRange = 5;

    [Header("Phone")] 
    [SerializeField] private float targetZoom;
    private const int ZOOM_SPEED = 2;
    private bool lookingAtPhone;
    private bool zooming;

    //Camera
    private Camera cam;
    private float rotX;
    private float rotY;
    private float prevRotX;
    private const float LOOK_BACK_THRESH = 80f;



    private void Start()
    {
        cam = transform.GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void Update()
    {
        prevRotX = rotX;
        
        rotX += Input.GetAxis("Mouse X") * rotationSpeed;
        rotY += Input.GetAxis("Mouse Y") * rotationSpeed;

        rotX = Mathf.Clamp(rotX, clampX.x, clampX.y);
        rotY = Mathf.Clamp(rotY, clampY.x, clampY.y);
        
        //Rotate cam and empty
        // cam.transform.localRotation = Quaternion.Euler(-rotY,rotX,0f);
        // camOffset.transform.localRotation = Quaternion.Euler(0f,0f,rotX*.1f*leanFactor);
        // transform.localRotation = Quaternion.Euler(0f,0f,-rotX*.1f*leanFactor);
        cam.transform.localRotation = Quaternion.Euler(-rotY,rotX,0f);
        camOffset.transform.localPosition = new Vector3(rotX/clampX.y*leanFactor,1.561f,0f);
        
        CheckLookingBack();
        CheckForInteraction();
    }

    private void FixedUpdate()
    {
        if (lookingAtPhone)
        {
            if (zooming)
            {
                if (cam.fieldOfView > targetZoom && cam.fieldOfView - ZOOM_SPEED >= targetZoom)
                    cam.fieldOfView -= ZOOM_SPEED;
                else
                {
                    cam.fieldOfView = targetZoom;
                    zooming = false;
                }
            }
        }
    }

    private void CheckForInteraction()
    {
        Transform camTransform = cam.transform;

        bool hitPhone = false;
        
        if (Physics.Raycast(camTransform.position, camTransform.forward, out RaycastHit hit, interactionRange))
        {
            if (Input.GetMouseButtonDown(0)) {
                hit.collider.gameObject.GetComponent<Interactable.Interactable>()?.Interact();
            }
            
            if (hit.collider.gameObject.name == "phone")
            {
                hitPhone = true;
                if (Math.Abs(cam.fieldOfView - fov) < 0.01f)
                {
                    lookingAtPhone = true;
                    zooming = true;
                }
            }
        }
        
        if (!hitPhone && lookingAtPhone)
        {
            lookingAtPhone = false;
            cam.fieldOfView = fov;
        }


        #if UNITY_EDITOR
        Debug.DrawRay(camTransform.position, camTransform.forward * interactionRange, Color.red);
        #endif
    }

    private void CheckLookingBack()
    {
        bool rotOver = rotX > LOOK_BACK_THRESH;
        bool prevRotOver = prevRotX > LOOK_BACK_THRESH;

        if (rotOver && !prevRotOver) ManagerOfEvents.instance.TriggerEvent(ManagerOfEvents.CustomEvent.StartedLookingBackwards);
        else if (!rotOver && prevRotOver) ManagerOfEvents.instance.TriggerEvent(ManagerOfEvents.CustomEvent.StoppedLookingBackwards);
    }
}

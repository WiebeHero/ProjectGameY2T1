using System;
using DG.Tweening;
using Managers;
using UnityEngine;
using Cursor = UnityEngine.Cursor;

public sealed class CameraControl : MonoBehaviour
{
    public static bool active { get; private set; }
    
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
    private const int ZOOM_IN_SPEED = 2;
    private const int ZOOM_OUT_SPEED = 3;
    private bool lookingAtPhone;
    private bool zoomingOut;
    private bool zoomingIn;

    private bool hasNotification;

    //Camera
    private Camera cam;
    private float rotX;
    private float rotY;
    private float prevRotX;
    private const float LOOK_BACK_THRESH = 100f;

    public static void SetActive(bool newState) => active = newState;


    private static bool panning;
    //private Vector3 targetRotation;
    //private float panDuration;
    public bool DonePanning() => !panning;

    public void PanTowards(Vector3 targetRotation, float duration)
    {
        // panning = true;
        // targetRotation = targetRotation_;
        // panDuration = duration_;
        camOffset.transform.DOLocalMoveX(0, duration);
        cam.transform.DORotate(targetRotation,duration).onComplete += () => panning = false;

    }
    
    private void Start()
    {
        active = true;
        cam = transform.GetComponentInChildren<Camera>();
        InformationManager.cursorLockMode = CursorLockMode.Locked;
    }
    
    private void Update()
    {
        CheckForInteraction();

        if (!active) return;

        prevRotX = rotX;
        
        rotX += Input.GetAxis("Mouse X") * rotationSpeed;
        rotY += Input.GetAxis("Mouse Y") * rotationSpeed;

        rotX = Mathf.Clamp(rotX, clampX.x, clampX.y);
        rotY = Mathf.Clamp(rotY, clampY.x, clampY.y);
        
        //Rotate cam and empty
        cam.transform.localRotation = Quaternion.Euler(-rotY,rotX,0f);
        camOffset.transform.localPosition = new Vector3(rotX/clampX.y*leanFactor,1.561f,0f);
        
        CheckLookingBack();
    }

    private void Pan()
    {
    }

    private void FixedUpdate()
    {
        if (lookingAtPhone && zoomingIn)
        {
            if (cam.fieldOfView > targetZoom && cam.fieldOfView - ZOOM_IN_SPEED >= targetZoom)
                cam.fieldOfView -= ZOOM_IN_SPEED;

            else {
                cam.fieldOfView = targetZoom;
                zoomingIn = false;
            }
        }
        else if (zoomingOut)
        {
            if (cam.fieldOfView < fov && cam.fieldOfView + ZOOM_OUT_SPEED <= fov)
                cam.fieldOfView += ZOOM_OUT_SPEED;
            
            else {
                cam.fieldOfView = fov;
                zoomingOut = false;
            }
        }
    }

    private void CheckForInteraction()
    {

        Input.GetMouseButton(0);
        
        Transform camTransform = cam.transform;

        bool hitPhone = false;
        
        if (Physics.Raycast
                (camTransform.position, camTransform.forward, 
                    out RaycastHit hit, interactionRange)
            )
        {
            if (Input.GetMouseButtonDown(0)) 
                hit.collider.gameObject.GetComponent<Interactable.Interactable>()?.OnLeft();
            if (Input.GetMouseButton(0)) 
                hit.collider.gameObject.GetComponent<Interactable.Interactable>()?.OnLeftHold();

            if (hit.collider.gameObject.name == "phone")
            {
                hitPhone = true;
                if (Math.Abs(cam.fieldOfView - fov) < 0.01f)
                {
                    EventHub.TriggerEvent
                        (EventHub.CustomEvent.StartedLookingAtPhone);
                    
                    lookingAtPhone = true;
                    zoomingIn = true;
                }
            }
        }
        
        if (!hitPhone && lookingAtPhone)
        {
            EventHub.TriggerEvent
                (EventHub.CustomEvent.StoppedLookingAtPhone);
            lookingAtPhone = false;
            zoomingOut = true;
        }
        
        #if UNITY_EDITOR
        Debug.DrawRay(camTransform.position, camTransform.forward * interactionRange, Color.red);
        #endif
    }

    private void CheckLookingBack()
    {
        bool rotOver = rotX > LOOK_BACK_THRESH;
        bool prevRotOver = prevRotX > LOOK_BACK_THRESH;

        if (rotOver && !prevRotOver)
            EventHub.TriggerEvent
                (EventHub.CustomEvent.StartedLookingBackwards);
        
        else if (!rotOver && prevRotOver) 
            EventHub.TriggerEvent
                (EventHub.CustomEvent.StoppedLookingBackwards);
    }
}

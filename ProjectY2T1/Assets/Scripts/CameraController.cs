using System;
using DG.Tweening;
using Managers;
using UnityEngine;
using static Managers.UIManager.GUI;

public sealed class CameraController : MonoBehaviour
{

    public static CameraController i;
    public static bool active;
    
    [SerializeField] private Transform camOffset;

    [SerializeField] private float fov = 60f;
    
    [Header("Factors")]
    public static float rotationSpeed = 10;
    [SerializeField] private float leanFactor = 2;
    
    [Header("Clamp Values")]
    [SerializeField] private Vector2 clampX;
    [SerializeField] private Vector2 clampY;


    [Header("Interaction")]
    [SerializeField] private float interactionRange = 5;
    
    //Camera
    [NonSerialized] public Camera cam;
    private float rotX;
    private float rotY;
    private float prevRotX;
    private const float LOOK_BACK_THRESH = 100f;

    //Temporary event variables
    private GameObject lastHitObject;
    private bool left;

    public static void SetActive(bool newState) => active = newState;
    
    private static bool panning;
    public static bool DonePanning() => !panning;

    public void PanTowards(Vector3 targetRotation, float duration)
    {
        camOffset.transform.DOLocalMoveX(0, duration);
        cam.transform.DORotate(targetRotation,duration).onComplete += () => panning = false;
    }
    
    public void MoveTowards(Vector3 targetPosition, float duration) => cam.transform.DOLocalMove(targetPosition, duration);

    public void Zoom(float newFov, float duration) => 
        DOTween.To(() => cam.fieldOfView, x => cam.fieldOfView = x, newFov, duration);

    private void Awake()
    {
        if (i != null && i != this) Destroy(this);
        i = this;
        InformationManager.isCrashing = false;
        active = true;
        cam = transform.GetComponentInChildren<Camera>();
        InformationManager.cursorLockMode = CursorLockMode.Locked;
    }
    
    private void Update()
    {
        if (InformationManager.isCrashing) return;
       
        KeyBoardInputs();

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
    

    private void KeyBoardInputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UIManager.openGUI == Menu) UIManager.NoGUI();
            else UIManager.OpenMenu();
        } 
        //UIManager.i.OpenGUI(UIManager.openGUI == Menu ? None : Menu);

        if (Input.GetKeyDown(KeyCode.A)) 
            SceneSwapper.i.SwapScene(InformationManager.Scene.MainMenu);
    }

    private void FixedUpdate()
    {
        if (!InformationManager.isCrashing) CheckForInteraction();
    }

    private void CheckForInteraction()
    {
        Transform camTransform = cam.transform;

        if (Physics.Raycast
            (camTransform.position, camTransform.forward, 
                out RaycastHit hit, interactionRange)
           )
        {
            GameObject hitObject = hit.collider.gameObject;
            if (lastHitObject == null)
            {
                lastHitObject = hit.collider.gameObject;
                hitObject.GetComponent<Interactable>()?.OnLookAt();
            }

            if (Input.GetMouseButtonDown(0))
            {
                hitObject.GetComponent<Interactable>()?.OnLeft();
                left = true;
            }
            
            else if (Input.GetMouseButton(0))
                hitObject.GetComponent<Interactable>()?.OnLeftHold();
            
            else if (left)
            {
                hitObject.GetComponent<Interactable>()?.OnLeftRelease();
                left = false;
            }
        }
        else
        {
            if (lastHitObject != null)
            {
                lastHitObject.GetComponent<Interactable>()?.OnStopLookAt();
                if (left)
                {
                    lastHitObject.GetComponent<Interactable>()?.OnLeftRelease();
                }
                lastHitObject = null;
            }
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

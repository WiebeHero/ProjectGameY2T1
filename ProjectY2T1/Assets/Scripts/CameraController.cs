using System;
using DG.Tweening;
using Managers;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static Managers.UIManager.GUI;

public sealed class CameraController : MonoBehaviour
{

    public static CameraController i;
    public static bool active;
    
    [SerializeField] private Transform camOffset;
    
    [Header("Factors")]
    public static float rotationSpeed = 10;
    [SerializeField] private float leanFactor = 2;
    
    [Header("Clamp Values")]
    [SerializeField] private Vector2 clampX;
    [SerializeField] private Vector2 clampY;


    [Header("Interaction")]
    [SerializeField] private float interactionRange = 5;

    [Header("Pointer")] 
    [SerializeField] private GameObject pointerA;
    [SerializeField] private GameObject pointerB;
    private bool pointing;
    private bool pointersActive;
    private bool firstLook;
    
    //Camera
    [NonSerialized] public Camera cam;
    private float rotX;
    private float rotY;
    private float prevRotX;
    private const float LOOK_BACK_THRESH = 100f;

    //Temporary event variables
    private GameObject lastHitObject;
    private bool holdingLMB;

    public static void SetActive(bool newState) => active = newState;

    public void PanTowards(Vector3 targetRotation, float duration)
    {
        camOffset.transform.DOLocalMoveX(0, duration);
        cam.transform.DORotate(targetRotation,duration).onComplete += () => { };
    }
    
    public void MoveTowards(Vector3 targetPosition, float duration) => cam.transform.DOLocalMove(targetPosition, duration);

    public void Zoom(float newFov, float duration)
    {
        DOTween.KillAll();
        DOTween.To(() => cam.fieldOfView, x => cam.fieldOfView = x, newFov, duration)
            .onComplete += () =>
        {
            cam.fieldOfView = newFov;
            Debug.Log("Final FOV: " + cam.fieldOfView);
        };

    }

    private void Awake()
    {
        if (i != null && i != this) Destroy(this);
        i = this;

        cam = transform.GetComponentInChildren<Camera>();
        if (cam == null) throw new Exception("No camera object found by controller!");
    }
    
    private void Start()
    {
        InformationManager.isCrashing = false;
        active = true;
        
        InformationManager.cursorLockMode = CursorLockMode.Locked;

        EventHub.CarCrashStartEvent += () =>
        {
            Debug.Log(pointerA);
            pointerA.SetActive(false);
            
            pointerB.SetActive(false);
        };
    }
    
    private void Update()
    {
        if (InformationManager.isCrashing) return;
        CheckForInteraction();
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
    }

    private void CheckForInteraction()
    {
        Transform camTransform = cam.transform;

        
        if (Physics.Raycast
            (camTransform.position, camTransform.forward, 
                out RaycastHit hit, interactionRange))
        {

            GameObject hitObject = hit.collider.gameObject;
            if (hitObject != null) lastHitObject = hitObject;

            Interactable interactable = hitObject.GetComponent<Interactable>();
            if (interactable == null) return;


            if (!pointing)
            {
                pointerB.SetActive(true);
                pointing = true;
            }

            if (!firstLook)
            {
                interactable.OnLookAt();
                firstLook = true;
            }
            

            if (Input.GetMouseButtonDown(0))
            {
                interactable.OnLMB();
                holdingLMB = true;
            }
            
            else if (Input.GetMouseButton(0))
                interactable.OnLMBHold();
            
            else if (holdingLMB)
            {
                interactable.OnLMBRelease();
                holdingLMB = false;
            }
        }
        else
        {
            if (pointing)
            {
                pointerB.SetActive(false);
                pointing = false;
            }
            
            
            if (lastHitObject != null)
            {
                Interactable interactable = lastHitObject.GetComponent<Interactable>();
                if (interactable != null)
                {
                    interactable.OnStopLookAt();
                    firstLook = false;
                    if (holdingLMB) interactable.OnLMBRelease();
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

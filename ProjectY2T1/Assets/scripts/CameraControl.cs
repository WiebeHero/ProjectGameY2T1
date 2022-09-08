using Managers;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class CameraControl : MonoBehaviour
{
    [Header("Factors")]
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private float leanFactor = 1;
    
    [Header("Clamp Values")]
    [SerializeField] private Vector2 clampX;
    [SerializeField] private Vector2 clampY;
    
    [Header("Interaction")]
    [SerializeField] private float interactionRange = 5;

    private Transform cam;
    private float rotX;
    private float rotY;

    private float prevRotX;
    
    private const float LOOK_BACK_THRESH = 80f;

    private void Start()
    {
        cam = transform.GetComponentInChildren<Camera>().gameObject.transform;
        Cursor.lockState = CursorLockMode.Locked; 
        
        EventManager.instance.StartedLookingBackwardsEvent += TestLookingBack;
        EventManager.instance.StoppedLookingBackwardsEvent += TestStopLookingBack;
    }
    
    private void Update()
    {
        prevRotX = rotX;
        
        Transform mainTransform = transform;
        
        rotX += Input.GetAxis("Mouse X") * rotationSpeed;
        rotY += Input.GetAxis("Mouse Y") * rotationSpeed;

        rotX = Mathf.Clamp(rotX, clampX.x, clampX.y);
        rotY = Mathf.Clamp(rotY, clampY.x, clampY.y);
        
        cam.transform.localRotation = Quaternion.Euler(-rotY,rotX,0f);
        mainTransform.localRotation = Quaternion.Euler(0f,0f,-rotX * .1f * leanFactor);
        
        CheckLookingBack();

        // Interaction Debug
        { 
            #if UNITY_EDITOR
                Transform camTransform = cam.transform;
                Debug.DrawRay(camTransform.position, camTransform.forward * interactionRange, Color.red);
            #endif
        }

        if (Input.GetMouseButtonDown(0)) CheckForInteraction();
    }

    private void CheckForInteraction()
    {
        Transform camTransform = cam.transform;
        
        if (Physics.Raycast(camTransform.position, camTransform.forward, out RaycastHit hit, interactionRange))
        {
            Interactable.Interactable interactable = hit.collider.gameObject.GetComponent<Interactable.Interactable>();
            if (interactable != null) interactable.Interact();
        }
    }

    private void CheckLookingBack()
    {
        bool rotOver = rotX > LOOK_BACK_THRESH;
        bool prevRotOver = prevRotX > LOOK_BACK_THRESH;

        if (rotOver && !prevRotOver) EventManager.instance.TriggerEvent(EventManager.CustomEvent.StartedLookingBackwards);
        else if (!rotOver && prevRotOver) EventManager.instance.TriggerEvent(EventManager.CustomEvent.StoppedLookingBackwards);
    }

    private void TestLookingBack()
    {
        Debug.LogWarning("LOOKING BACK");
    }

    private void TestStopLookingBack()
    {
        Debug.LogWarning("STOPPED LOOKING BACK");
    }
}

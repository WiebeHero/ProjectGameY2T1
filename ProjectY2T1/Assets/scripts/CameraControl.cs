using UnityEngine;

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

    private void Start()
    {
        cam = transform.GetComponentInChildren<Camera>().gameObject.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Transform camTransform = cam.transform;
        Transform mainTransform = transform;
        
        rotX += Input.GetAxis("Mouse X") * rotationSpeed;
        rotY += Input.GetAxis("Mouse Y") * rotationSpeed;

        rotX = Mathf.Clamp(rotX, clampX.x, clampX.y);
        rotY = Mathf.Clamp(rotY, clampY.x, clampY.y);
        
        camTransform.localRotation = Quaternion.Euler(-rotY,rotX,0f);
        mainTransform.localRotation = Quaternion.Euler(0f,0f,-rotX * .1f * leanFactor);

        #if UNITY_EDITOR
            Debug.DrawRay(camTransform.position, camTransform.forward * interactionRange, Color.red);
        #endif
        
        if (Physics.Raycast(camTransform.position, camTransform.forward, out RaycastHit hit, interactionRange))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Interactable.Interactable interactable = hit.collider.gameObject.GetComponent<Interactable.Interactable>();
                if (interactable != null) interactable.Interact();
            }
        }
    }
}

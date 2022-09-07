using UnityEngine;

public class PlayerControls : MonoBehaviour
{
	//Mouse sensitivity, always higher than 0
	[SerializeField] [Min(0)] private float sensitivity = 10;

	[SerializeField] private float maxHorizontalLookAngle = 120;
	[SerializeField] private float maxVerticalLookAngle = 50f;
	[SerializeField] private Camera firstPersonCamera;
	
	private float yaw;
	private float pitch;

	private float yawCameraVelocity;
	private float pitchCameraVelocity;
	
	private void Start()
	{
		if (firstPersonCamera == null)
		{
			Debug.LogWarning("No first person camera assigned! Trying if parent is the camera.");
			firstPersonCamera = gameObject.GetComponentInChildren<Camera>();
			
			if (firstPersonCamera == null) Debug.LogError("No first person camera found either!");
		}

		Cursor.lockState = CursorLockMode.Locked; 
	}
		

	private void Update()
	{
		//yaw += Input.GetAxis("Mouse X") * sensitivity;
		yaw = Mathf.SmoothDamp(yaw, (yaw + Input.GetAxis("Mouse X") * sensitivity), ref yawCameraVelocity, 0.01f);
		yaw = Mathf.Clamp(yaw, -maxHorizontalLookAngle, maxHorizontalLookAngle);

		//pitch -= Input.GetAxis("Mouse Y") * sensitivity;
		pitch -= Mathf.SmoothDamp(0, Input.GetAxis("Mouse Y") * sensitivity, ref pitchCameraVelocity, 0.01f);
		pitch = Mathf.Clamp(pitch, -maxVerticalLookAngle - 20, maxVerticalLookAngle);
		
		firstPersonCamera.transform.localEulerAngles = new Vector3(pitch, yaw, 0);
	}
}
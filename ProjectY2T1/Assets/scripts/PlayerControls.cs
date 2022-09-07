using UnityEngine;

public class PlayerControls : MonoBehaviour
{
	//Mouse sensitivity, always higher than 0
	[SerializeField] [Min(0)] private float sensitivity;

	[SerializeField] private float maxHorizontalLookAngle;
	[SerializeField] private float maxVerticalLookAngle;
	private float maxHorizontal = 0.0F;
	private float maxVertical = 0.0F;
	private float minHorizontal = 0;
	private float minVertical = 0;

	private float yaw;
	private float pitch;

	private float yawCameraVelocity;
	private float pitchCameraVelocity;
	
	private void Start()
	{
		this.yaw = this.transform.localEulerAngles.y;
		this.pitch = this.transform.localEulerAngles.x;

		this.maxHorizontal = this.yaw + maxHorizontalLookAngle / 2;
		this.minHorizontal = this.yaw - maxHorizontalLookAngle / 2;
		this.maxVertical = this.pitch + maxVerticalLookAngle / 2;
		this.minVertical = this.pitch - maxVerticalLookAngle / 2;
		Cursor.lockState = CursorLockMode.Locked; 
	}
		

	private void Update()
	{
		//yaw += Input.GetAxis("Mouse X") * sensitivity;
		yaw = Mathf.SmoothDamp(yaw, (yaw + Input.GetAxis("Mouse X") * sensitivity), ref yawCameraVelocity, 0.01f);
		yaw = Mathf.Clamp(yaw, this.minHorizontal, this.maxHorizontal);

		//pitch -= Input.GetAxis("Mouse Y") * sensitivity;
		pitch -= Mathf.SmoothDamp(0, Input.GetAxis("Mouse Y") * sensitivity, ref pitchCameraVelocity, 0.01f);
		pitch = Mathf.Clamp(pitch, this.minVertical, this.maxVertical);

		this.transform.eulerAngles = new Vector3(pitch, yaw, 0.0F);
	}
}
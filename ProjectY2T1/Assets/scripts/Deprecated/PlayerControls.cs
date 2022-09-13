using System;
using UnityEngine;

namespace Deprecated
{
	[Obsolete]
	public class PlayerControls : MonoBehaviour
	{
		//Mouse sensitivity, always higher than 0
		[SerializeField] [Min(0)] private float sensitivity;
		[SerializeField] private float maxHorizontalLookAngle = 120;
		[SerializeField] private float maxVerticalLookAngle = 50f;
		[SerializeField] private Camera firstPersonCamera;

		[SerializeField] private float interactionRange = 5;
		private float yaw;
		private float pitch;

		private float yawCameraVelocity;
		private float pitchCameraVelocity;
	
		private void Start()
		{
			this.yaw = this.transform.localEulerAngles.y;
			this.pitch = this.transform.localEulerAngles.x;

			Cursor.lockState = CursorLockMode.Locked; 
		}


		private void Update()
		{
			//yaw += Input.GetAxis("Mouse X") * sensitivity;
			yaw = Mathf.SmoothDamp(yaw, (yaw + Input.GetAxis("Mouse X") * sensitivity), ref yawCameraVelocity, 0.01f);
			yaw = Mathf.Clamp(yaw, -maxHorizontalLookAngle, maxHorizontalLookAngle + 50);

			if (yaw > maxHorizontalLookAngle)
			{

			}

			//pitch -= Input.GetAxis("Mouse Y") * sensitivity;
			pitch -= Mathf.SmoothDamp(0, Input.GetAxis("Mouse Y") * sensitivity, ref pitchCameraVelocity, 0.01f);
			pitch = Mathf.Clamp(pitch, -maxVerticalLookAngle - 20, maxVerticalLookAngle);

			firstPersonCamera.transform.localEulerAngles = new Vector3(pitch, yaw, 0);


			/*
		 * Interaction system
		 */
			Transform transform1 = transform;

			Debug.DrawRay(transform1.position, transform1.forward * interactionRange, Color.red);
			if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactionRange))
			{
				Interactable.Interactable interactable = hit.collider.gameObject.GetComponent<Interactable.Interactable>();
				if (interactable != null) interactable.OnLeft();
			}
		}
	}
}
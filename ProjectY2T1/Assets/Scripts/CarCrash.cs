using System;
using System.Collections;
using TerrainMovement;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Cursor = UnityEngine.Cursor;

public sealed class CarCrash : MonoBehaviour
{
	
	[Header("Panning towards windshield")]
	[SerializeField] private CameraControl cameraControl;
	[SerializeField] private float panDuration;
	[SerializeField] private Vector3 panTarget;

	[Header("Animator")] [SerializeField] private Animator animator;
	
	[Header("Choice buttons")]
	[SerializeField] private GameObject grandmaButtonObject;
	[SerializeField] private GameObject childButtonObject;

	private GameObject parentObject;
	private Button grandmaButton;
	private Button childButton;


	private void Start()
	{
		if (cameraControl == null) throw new Exception("Car crash script has no camera control attached");
		if (grandmaButtonObject == null) throw new Exception("Grandma button object has not been attached");
		if (childButtonObject == null) throw new Exception("Child button object has not been attached");

		//Check buttons
		grandmaButton = grandmaButtonObject.GetComponent<Button>();
		if (grandmaButton == null) throw new Exception("Attached grandma button object doesn't have a button component");

		childButton = childButtonObject.GetComponent<Button>();
		if (childButton == null) throw new Exception("Attached Child button object doesn't have a button component");
		
		
		//Check parent object
		parentObject = grandmaButtonObject.transform.parent.gameObject;
		if (parentObject == null) throw new Exception("Car crash couldn't find parent object of grandma object");

		GameObject childObject = childButtonObject.transform.parent.gameObject;
		if (!parentObject.Equals(childObject)) 
			throw new Exception("Grandma object and child object should have the same parent object");

		grandmaButton.onClick.AddListener(OnChooseGrandma);
		childButton.onClick.AddListener(OnChooseChild);
	}
	
	public void Run() => StartCoroutine(Crash());
	
	private IEnumerator Crash()
	{
		//Wait for the camera to pan towards the windshield
		CameraControl.SetActive(false);
		cameraControl.PanTowards(panTarget, panDuration);
		yield return new WaitUntil(cameraControl.DonePanning);
		
		parentObject.SetActive(true);		
		Cursor.lockState = CursorLockMode.Confined;
		MovingTerrainManager.speedMode = MovingTerrainManager.SpeedMode.Slow;
	}
	
	private void OnChooseGrandma()
	{
		Close();
		MovingTerrainManager.speedMode = MovingTerrainManager.SpeedMode.Frozen;
		animator.SetTrigger("CrashGrandma");
	}
	
	private void OnChooseChild()
	{
		Close();
		MovingTerrainManager.speedMode = MovingTerrainManager.SpeedMode.Frozen;
		animator.SetTrigger("CrashKids");
	}

	private void Close()
	{
		parentObject.SetActive(false);
		Cursor.lockState = CursorLockMode.Locked;
	}
}
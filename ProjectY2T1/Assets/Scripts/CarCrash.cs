using System;
using System.Collections;
using Managers;
using TerrainMovement;
using UnityEngine;
using UnityEngine.UI;

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

	[Header("Grandma and Children")] 
	[SerializeField] private GameObject grandma;
	[SerializeField] private GameObject child;
	[SerializeField] private GameObject child2;
	

	private GameObject parentObject;
	private Button grandmaButton;
	private Button childButton;
	
	private static readonly int CrashGrandma = Animator.StringToHash("CrashGrandma");
	private static readonly int CrashKids = Animator.StringToHash("CrashKids");


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
	
	public void Run()
	{
		InformationManager.isCrashing = true;
		StartCoroutine(Crash());
	}

	private IEnumerator Crash()
	{
		//Wait for the camera to pan towards the windshield
		CameraControl.SetActive(false);
		cameraControl.PanTowards(panTarget, panDuration);
		yield return new WaitUntil(cameraControl.DonePanning);
		
		parentObject.SetActive(true);
		InformationManager.cursorLockMode = CursorLockMode.Confined;
		MovingTerrainManager.speedMode = MovingTerrainManager.SpeedMode.Slow;
	}
	
	private void OnChooseGrandma()
	{
		Close();
		MovingTerrainManager.speedMode = MovingTerrainManager.SpeedMode.Frozen;
		if (grandma == null) throw new Exception("The grandma is not assigned!");
		Rigidbody rigid = grandma.GetComponent<Rigidbody>();
		if (rigid != null)
		{
			rigid.constraints = RigidbodyConstraints.None;
			rigid.useGravity = true;
		}
		else
		{
			throw new Exception("The grandma has no rigidbody assigned!");
		}
		animator.SetTrigger("CrashGrandma");
	}
	
	private void OnChooseChild()
	{
		Close();
		MovingTerrainManager.speedMode = MovingTerrainManager.SpeedMode.Frozen;
		if (child == null || child2 == null) throw new Exception("One of the children is not assigned!");
		Rigidbody rigid = child.GetComponent<Rigidbody>();
		Rigidbody rigid2 = child2.GetComponent<Rigidbody>();
		if (rigid != null && rigid2 != null)
		{
			rigid.constraints = RigidbodyConstraints.None;
			rigid2.constraints = RigidbodyConstraints.None;
			rigid.useGravity = true;
			rigid2.useGravity = true;
		}
		else
		{
			throw new Exception("Either 1 or both kids have no rigidbody assigned!");
		}
		animator.SetTrigger("CrashKids");
	}

	private void Close()
	{
		parentObject.SetActive(false);
		InformationManager.cursorLockMode = InformationManager.prevCursorLockMode;
	}
}
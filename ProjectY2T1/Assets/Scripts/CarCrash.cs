using System;
using System.Collections;
using Managers;
using TerrainMovement;
using UnityEngine;
using UnityEngine.UI;

public sealed class CarCrash : MonoBehaviour
{
	
	[Header("Panning towards windshield")]
	[SerializeField] private float panDuration;
	[SerializeField] private Vector3 panTarget;

	[Header("Move forwards")] 
	[SerializeField] private float moveDuration;
	[SerializeField] private Vector3 moveTarget;

	[Header("Animator")] [SerializeField] private Animator animator;
	
	[Header("Choice buttons")]
	[SerializeField] private GameObject grandmaButtonObject;
	[SerializeField] private GameObject childButtonObject;

	[Header("Grandma and Children")] 
	[SerializeField] private GameObject grandma;
	[SerializeField] private GameObject child;
	[SerializeField] private GameObject child2;

	[Header("SadStuff")] 
	[SerializeField] private CoolText coolText;


	private CameraController cameraController;
	
	private GameObject parentObject;
	private Button grandmaButton;
	private Button childButton;

	private float originalCamFov;
	private static readonly int CrashKids = Animator.StringToHash("CrashKids");
	private static readonly int CrashGrandma = Animator.StringToHash("CrashGrandma");

	public static CarCrash i { get; private set; }

	private void Awake()
	{
		if (i != null && i != this) Destroy(this);
		i = this;
	}

	private void Start()
	{
		if (grandmaButtonObject == null) throw new Exception("Grandma button object has not been attached");
		if (childButtonObject == null) throw new Exception("Child button object has not been attached");

		cameraController = CameraController.i;
		if (cameraController == null) throw new Exception("No camera controller in scene found by CarCrash.cs");

		originalCamFov = cameraController.cam.fieldOfView;

		grandmaButton = grandmaButtonObject.GetComponent<Button>();
		if (grandmaButton == null) throw new Exception("Attached grandma button object doesn't have a button component");

		childButton = childButtonObject.GetComponent<Button>();
		if (childButton == null) throw new Exception("Attached Child button object doesn't have a button component");
		
		parentObject = grandmaButtonObject.transform.parent.gameObject;
		if (parentObject == null) throw new Exception("Car crash couldn't find parent object of grandma object");

		GameObject childObject = childButtonObject.transform.parent.gameObject;
		if (!parentObject.Equals(childObject)) 
			throw new Exception("Grandma object and child object should have the same parent object");

		grandmaButton.onClick.AddListener(OnChooseGrandma);
		childButton.onClick.AddListener(OnChooseChild);

		AudioSource source = GetComponent<AudioSource>();
		if (source == null) throw new Exception("Audio source component is not present!");
		if (source.clip != null) EventHub.CarCrashStartEvent += source.Stop;
	}
	
	public void Run()
	{
		InformationManager.isCrashing = true;
		Crash();
	}

	private void Crash()
	{
		coolText.StartSchmoovin();

		if (Math.Abs(cameraController.cam.fieldOfView - originalCamFov) > 0.0001f) 
			cameraController.Zoom(originalCamFov, 0.2f);

		CameraController.SetActive(false);
		cameraController.MoveTowards(moveTarget,moveDuration);
		cameraController.PanTowards(panTarget, panDuration);
		
		parentObject.SetActive(true);
		InformationManager.cursorLockMode = CursorLockMode.Confined;
		MovingTerrainManager.speedMode = MovingTerrainManager.SpeedMode.Slow;
	}
	
	
  
	private void OnChooseGrandma()
	{
		FunFacts.chosenEnding = FunFacts.Ending.Grandma;
		
		if (grandma == null) throw new Exception("The grandma is not assigned!");
		Rigidbody rigid = grandma.GetComponent<Rigidbody>();
		if (rigid == null) throw new Exception("The grandma doesn't have a rigidbody!");
		
		Close();
		MovingTerrainManager.speedMode = MovingTerrainManager.SpeedMode.Frozen;
		rigid.constraints = RigidbodyConstraints.None;
		rigid.useGravity = true;
		
		animator.SetTrigger(CrashGrandma);
	}
	
	public void OnChooseChild()
	{
		FunFacts.chosenEnding = FunFacts.Ending.Kids;
		
		if (child == null || child2 == null) throw new Exception("One of the children is not assigned!");
		
		Rigidbody rigid = child.GetComponent<Rigidbody>();
		Rigidbody rigid2 = child2.GetComponent<Rigidbody>();
		if (rigid == null || rigid2 == null) throw new Exception("One of the children doesn't have a rigidbody!");
		
		Close();
		MovingTerrainManager.speedMode = MovingTerrainManager.SpeedMode.Frozen;
		
		rigid.constraints = RigidbodyConstraints.None;
		rigid2.constraints = RigidbodyConstraints.None;
		rigid.useGravity = true;
		rigid2.useGravity = true;
		
		animator.SetTrigger(CrashKids);
	}

	public void Close()
	{
		parentObject.SetActive(false);
		InformationManager.cursorLockMode = InformationManager.prevCursorLockMode;
	}
}
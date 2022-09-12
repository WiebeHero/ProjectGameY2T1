using System;
using TerrainMovement;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class CarCrash : MonoBehaviour
{
	[SerializeField] private Car car;
	[SerializeField] private GameObject grandmaButtonObject;
	[SerializeField] private GameObject childButtonObject;

	private GameObject parentObject;
	private Button grandmaButton;
	private Button childButton;
	

	private void Start()
	{
		if (car == null) throw new Exception("Car crash script has no car attached");
		if (grandmaButtonObject == null) throw new Exception("Grandma button object has not been attached");
		if (childButtonObject == null) throw new Exception("Child button object has not been attached");

		//Check buttons
		grandmaButton = grandmaButtonObject.GetComponent<Button>();
		if (grandmaButton == null) throw new Exception("Attached grandma button object doesn't have a button component");

		childButton = childButtonObject.GetComponent<Button>();
		if (childButton == null) throw new Exception("Attached Child button object doesn't have a button component");
		
		
		//Check parent object
		parentObject = grandmaButtonObject.GetComponentInParent<GameObject>();
		if (parentObject == null) throw new Exception("Carcrash couldn't find parent object of grandma object");

		GameObject childObject = childButtonObject.GetComponentInParent<GameObject>();
		if (!parentObject.Equals(childObject)) 
			throw new Exception("Grandma object and child object should have the same parent object");
	}

	
	

	public void Initiate()
	{
		parentObject.SetActive(true);		
		Cursor.lockState = CursorLockMode.Confined;
		MovingTerrainManager.Speed = 0;
	}


	private void Finish()
	{
		parentObject.SetActive(false);
	}
}
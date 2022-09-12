using System;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class CarCrash : MonoBehaviour
{
	[SerializeField] private Car car;
	[SerializeField] private GameObject grandmaButtonObject;
	[SerializeField] private GameObject kidButtonObject;

	private Button grandmaButton;
	private Button kidButton;
	

	private void Start()
	{
		if (car == null) throw new Exception("Car crash script has no car attached");
		if (grandmaButtonObject == null) throw new Exception("Grandma button object has not been attached");
		if (kidButtonObject == null) throw new Exception("Kid button object has not been attached");

		grandmaButton = grandmaButtonObject.GetComponent<Button>();
		if (grandmaButton == null) throw new Exception("Attached grandma button object doesn't have a button component");

		kidButton = kidButtonObject.GetComponent<Button>();
		if (kidButton == null) throw new Exception("Attached kid button object doesn't have a button component");
		
		
	}

	
	

	public void Initiate()
	{
		
		
		Cursor.lockState = CursorLockMode.Confined;
		MovingTerrainManager.Speed = 0;
	}


	private void Finish()
	{
		
	}
}
using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField] private GameObject menuObject;
	[SerializeField] private GameObject crossHair;
	[SerializeField] private Button continueButton;
	
	private void Start()
	{
		if (menuObject == null) throw new Exception("UIManager has no menuObject assigned!");
		if (crossHair == null) throw new Exception("UIManager has no crossHair assigned!");
		
		
	}

	public void ActivateMenu()
	{
		InformationManager.cursorLockMode = CursorLockMode.Confined;

		menuObject.SetActive(true);
		crossHair.SetActive(false);
	}

	public void DisableMenu()
	{
		InformationManager.cursorLockMode = InformationManager.prevCursorLockMode;
		
		menuObject.SetActive(false);
		crossHair.SetActive(true);
	}
}
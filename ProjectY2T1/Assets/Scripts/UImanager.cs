using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager i { get; private set; }
	
	[SerializeField] private GameObject menuObject;
	[SerializeField] private GameObject crossHair;

	[Header("Buttons")]
	[SerializeField] private Button continueButton;
	[SerializeField] private Button settingsButton;
	[SerializeField] private Button exitButton;

	private bool menuActive;

	private void Awake()
	{
		if (i != null && i != this) Destroy(this);
		i = this;
	}
	
	private void Start()
	{
		if (menuObject == null) throw new Exception("UIManager has no menuObject assigned!");
		if (crossHair == null) throw new Exception("UIManager has no crossHair assigned!");
		
		if (continueButton == null) throw new Exception("UIManager has no continueButton assigned!");
		if (settingsButton == null) throw new Exception("UIManager has no settingsButton assigned!");
		if (exitButton == null) throw new Exception("UIManager has no exitButton assigned!");
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (menuActive) DisableMenu();
			else ActivateMenu();
		}
		
		if (Input.GetKeyDown(KeyCode.A)) 
			SceneSwapper.i.SwapScene(InformationManager.Scene.MainMenu);
	}

	public void ActivateMenu()
	{
		InformationManager.cursorLockMode = CursorLockMode.Confined;
		InformationManager.paused = true;
		menuObject.SetActive(true);
		crossHair.SetActive(false);
		menuActive = true;
	}

	public void DisableMenu()
	{
		InformationManager.cursorLockMode = InformationManager.prevCursorLockMode;
		InformationManager.paused = false;
		menuObject.SetActive(false);
		crossHair.SetActive(true);
		menuActive = false;
	}
}
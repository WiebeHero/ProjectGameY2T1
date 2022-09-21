using System;
using UnityEngine;

namespace Managers
{
	public class UIManager : MonoBehaviour
	{
		public static UIManager i { get; private set; }
	
		[SerializeField] private GameObject menuObject;
		[SerializeField] private GameObject crossHair;
		[SerializeField] private GameObject confirmationObject;
		
		public static GUI openGUI;
		private bool menuActive;

		//Button functions
		public static void MenuContinue() => i.OpenGUI(GUI.None);

		public static void OpenMenu() => i.OpenGUI(GUI.Menu);
		public static void NoGUI() => i.OpenGUI(GUI.None);
		public static void OpenConfirmation() => i.OpenGUI(GUI.Confirmation);
		public static void CloseConfirmation()
		{
			Debug.LogWarning("does");
			i.OpenGUI(GUI.Menu);
		}

		public static void ConfirmExit() => SceneSwapper.i.SwapScene(InformationManager.Scene.MainMenu);

		private void Awake()
		{
			if (i != null && i != this) Destroy(this);
			i = this;
			
			if (menuObject == null) throw new Exception("UIManager has no menuObject assigned!");
			if (confirmationObject == null) throw new Exception("UIManager has no confirmationObject attached!");
			if (crossHair == null) throw new Exception("UIManager has no crossHair assigned!");
			
			openGUI = GUI.None;
		}

		[Serializable]
		public enum GUI
		{
			Menu,
			Confirmation,
			None
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.A)) OpenGUI(GUI.None);
		}

		private GameObject GUIEnumToGameObject(GUI gui)
		{
			return gui switch
			{
				GUI.Menu => menuObject,
				GUI.Confirmation => confirmationObject,
				GUI.None => null,
				_ => throw new ArgumentOutOfRangeException(nameof(gui), gui, null)
			};
		}
		
		public void OpenGUI(GUI gui)
		{
			switch (gui)
			{
				case GUI.None:
					if (openGUI != GUI.None) GUIEnumToGameObject(openGUI).SetActive(false);
					openGUI = GUI.None;
					InformationManager.cursorLockMode = CursorLockMode.Locked;
					InformationManager.paused = false;
					crossHair.SetActive(true);
					break;
	
				default:
					if (openGUI != GUI.None) GUIEnumToGameObject(openGUI).SetActive(false);
					openGUI = gui; 
					GUIEnumToGameObject(gui).SetActive(true);
					InformationManager.cursorLockMode = CursorLockMode.Confined;
					InformationManager.paused = true;
					crossHair.SetActive(false);
					break;
			}
		}
	}
}
using System;
using Managers;
using UnityEngine;

namespace UI
{
	public class UIManager : MonoBehaviour
	{
		public static UIManager i { get; private set; }
	
		[SerializeField] private GameObject menuObject;
		[SerializeField] private GameObject crossHair;
		[SerializeField] private GameObject settingsObject;

		public static GUI openGUI;
		private bool menuActive;

		//Button functions
		public static void MenuContinue() => UIManager.i.OpenGUI(GUI.None);
		public static void MenuSettings() => UIManager.i.OpenGUI(GUI.Settings);
		public static void MenuExit() => UIManager.i.OpenGUI(GUI.None);
		
		private void Awake()
		{
			if (i != null && i != this) Destroy(this);
			i = this;
		}
	
		private void Start()
		{
			if (menuObject == null) throw new Exception("UIManager has no menuObject assigned!");
			if (settingsObject == null) throw new Exception("UIManager has no settingsObject attached!");
			if (crossHair == null) throw new Exception("UIManager has no crossHair assigned!");
			
			openGUI = GUI.None;
		}

		[Serializable]
		public enum GUI
		{
			Menu,
			Settings,
			None
		}

		private GameObject GUIEnumToGameObject(GUI gui)
		{
			return gui switch
			{
				GUI.Menu => menuObject,
				GUI.Settings => settingsObject,
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
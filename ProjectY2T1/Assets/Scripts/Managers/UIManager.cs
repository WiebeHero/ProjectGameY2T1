using System;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
	public class UIManager : MonoBehaviour
	{
		public static UIManager i { get; private set; }
	
		[SerializeField] private GameObject menuObject;
		[SerializeField] private GameObject crossHair;
		[SerializeField] private GameObject settingsObject;

		[Header("Sliders")] 
		[SerializeField] private Slider sensitivitySlider;
		[SerializeField] private Slider volumeSlider;

		public static GUI openGUI;
		private bool menuActive;

		//Button functions
		public static void MenuContinue() => i.OpenGUI(GUI.None);
		public static void MenuSettings() => i.OpenGUI(GUI.Settings);
		public static void MenuExit() => SceneSwapper.i.SwapScene(InformationManager.Scene.MainMenu);
		public static void OpenMenu() => i.OpenGUI(GUI.Menu);
		public static void NoGUI() => i.OpenGUI(GUI.None);

		public void UpdateRotationSpeed() => CameraController.rotationSpeed = sensitivitySlider.value;
		// public void UpdateVolume() =>  = sensitivitySlider.value;

		private void Awake()
		{
			if (i != null && i != this) Destroy(this);
			i = this;
			
			if (menuObject == null) throw new Exception("UIManager has no menuObject assigned!");
			if (settingsObject == null) throw new Exception("UIManager has no settingsObject attached!");
			if (crossHair == null) throw new Exception("UIManager has no crossHair assigned!");
			
			openGUI = GUI.None;
			UpdateRotationSpeed();
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
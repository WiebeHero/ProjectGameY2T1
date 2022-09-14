using TerrainMovement;
using UnityEngine;

namespace Managers
{
	public static class InformationManager
	{
		private static bool isPaused;
		
		
		private static CursorLockMode pCursorLockMode;
		public static Scene currentScene;
		
		public static CursorLockMode cursorLockMode
		{
			get => pCursorLockMode;
			set
			{
				prevCursorLockMode = pCursorLockMode;
				pCursorLockMode = value;
				Cursor.lockState = pCursorLockMode;
			}
		}

		public static CursorLockMode prevCursorLockMode { get; private set; }

		public static bool paused
		{
			get => isPaused;
			set
			{
				isPaused = value;
				CameraControl.active = !isPaused;
				MovingTerrainManager.active = !isPaused;
			}
		}
		
		public enum Scene
		{
			MainMenu,
			Wiebe,
			Patrick
		}
	}
}

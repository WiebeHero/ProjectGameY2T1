using TerrainMovement;
using UnityEngine;

namespace Managers
{
	public static class InformationManager
	{
		private static bool isPaused;
		public static bool isCrashing;
		
		
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
				CameraController.active = !isPaused;
				MovingTerrainManager.active = !isPaused;
			}
		}
		
		public enum Scene
		{
			MainMenu,
			MainScene,
			Patrick
		}

		public static void Reset()
		{
			paused = false;
			cursorLockMode = CursorLockMode.Confined;
			isCrashing = false;
		}
	}
}

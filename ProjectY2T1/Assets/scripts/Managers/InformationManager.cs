using UnityEngine;

namespace Managers
{
	public static class InformationManager
	{
		private static CursorLockMode pCursorLockMode;
		
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
	}
}

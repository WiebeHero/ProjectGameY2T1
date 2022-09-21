using Managers;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	public static void StartGame()
	{
		InformationManager.Reset();
		SceneSwapper.i.SwapScene(InformationManager.Scene.Wiebe);
	}
}

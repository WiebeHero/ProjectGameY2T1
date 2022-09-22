using Managers;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private Texture2D mouseRed;
	[SerializeField] private Texture2D mouseNormal;
	public static void StartGame()
	{
		InformationManager.Reset();
		SceneSwapper.i.SwapScene(InformationManager.Scene.MainScene);
	}
	
	private void Start()
	{
		EventHub.PointerEnterButtonEvent += () => 
			Cursor.SetCursor(mouseRed, new Vector2(0,0), CursorMode.Auto);
			
		EventHub.PointerExitButtonEvent += () => 
			Cursor.SetCursor(mouseNormal, new Vector2(0,0), CursorMode.Auto);
	}
}

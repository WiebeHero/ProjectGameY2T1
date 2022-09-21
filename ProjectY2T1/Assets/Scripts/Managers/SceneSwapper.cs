using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
	public class SceneSwapper : MonoBehaviour
	{
		public static SceneSwapper i;

		private void Awake()
		{
			if (i != null && i != this) Destroy(this);
			i = this;
		}

		public void SwapScene(InformationManager.Scene scene)
		{
			string sceneName = Enum.GetName(typeof(InformationManager.Scene), scene);
			int buildIndex = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/" + sceneName + ".unity");
			if (buildIndex < 0) Debug.LogException(new Exception("No scene with name " + sceneName + " found"));
			
			//DontDestroyOnLoad(car);
			SceneManager.LoadSceneAsync(buildIndex);
			InformationManager.currentScene = scene;

			//SceneManager.sceneLoaded += (scene_, _) => 
			//SceneManager.MoveGameObjectToScene(car, SceneManager.GetSceneByBuildIndex(scene_.buildIndex));
		}
	}
}
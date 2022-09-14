using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
	public class SceneSwapper : MonoBehaviour
	{
		public static SceneSwapper i;

		[SerializeField] private GameObject car;
		private void Awake()
		{
			if (i != null && i != this) Destroy(this);
			i = this;

			if (car == null) throw new Exception("SceneSwapper needs an attached car object!");
		}

		public void SwapScene(InformationManager.Scene scene)
		{
			string sceneName = Enum.GetName(typeof(InformationManager.Scene), scene);
			int buildIndex = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/" + sceneName + ".unity");
			if (buildIndex < 0) Debug.LogException(new Exception("No scene with name " + sceneName + " found"));
			
			DontDestroyOnLoad(car);
			SceneManager.LoadSceneAsync(buildIndex);
			SceneManager.sceneLoaded += (scene_, _) => 
				SceneManager.MoveGameObjectToScene(car, SceneManager.GetSceneByBuildIndex(scene_.buildIndex));
		}
	}
}
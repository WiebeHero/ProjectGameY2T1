using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class FunFacts : MonoBehaviour
{
	[SerializeField] private GameObject funFactContainer;
	[SerializeField] private List<GameObject> imageObjects;
	[SerializeField] private List<GameObject> funFacts;
	[SerializeField] private float fadeDuration;
	[SerializeField] private Image mainMenuBackground;

	[NonSerialized] private Dictionary<string, GameObject> images;

	public bool active { get; private set; }
	
	private Image background;
	private GameObject endingObject;
	private Image ending;
	
	private GameObject selectedTextObject;
	private TextMeshProUGUI selectedText;
	
	private Random random;

	private bool imageTweenComplete;
	private bool imageGone;
	private bool textTweenComplete;

	public enum Ending
	{
		Grandma,
		Kids,
		Bunny,
		Good,
		Phone
	}

	public void ChooseEnding(Ending choice)
	{
		
	}
	
	public void Begin()
	{
		if (active) return;
		active = true;
		
		ChooseEnding("TestTestTest");

		if (endingObject != null)
		{
			endingObject.SetActive(true);
			ending = endingObject.GetComponent<Image>();
		}
		else Debug.LogWarning("No image chosen for the 'fun' facts");
		
		funFactContainer.SetActive(true);
		
		background.DOFade(1, fadeDuration).onComplete += () =>
		{
			ending.DOFade(1, fadeDuration).onComplete += () => imageTweenComplete = true;
		};
	}

	private void ChooseEnding(string imageName)
	{
		if (!images.ContainsKey(imageName))
			Debug.LogWarning($"Images doesn't contain key: {imageName}!");

		else endingObject = images[imageName];
	}
	
	private void Update()
	{
		// if (funFactContainer != null) Debug.LogWarning("Ja");
		
		if (active)
		{
			if (Input.anyKeyDown)
			{
				if (imageTweenComplete && !imageGone) {
					RemoveImage();
					imageGone = true;
				}
				else if (textTweenComplete) EndSequence();
			}
		}
		//Testing
		else if (Input.GetKeyDown(KeyCode.Space)) Begin();
	}

	private void Start()
	{
		if (funFactContainer == null) 
			throw new Exception("FunFacts script doesn't know where the fun facts are :(");

		background = funFactContainer.GetComponentInChildren<Image>();
		if (background == null) throw new Exception("FunFacts can't find a background");
		
		active = false;
		EventHub.CarCrashStopEvent += Begin;

		images = new Dictionary<string, GameObject>();
		
		foreach (GameObject imageObject in imageObjects)
		{
			Image image = imageObject.GetComponent<Image>();
			
			if (image == null) Debug.LogWarning(
				$"ImageObject with name '{imageObject.name}' doesn't have an image component!");
			
			else images.Add(imageObject.name, imageObject);
		}
	}

	private void EndSequence()
	{
		selectedText.DOFade(0, fadeDuration).onComplete += () =>
		{
			SceneSwapper.i.SwapScene(InformationManager.Scene.MainMenu);
			InformationManager.cursorLockMode = CursorLockMode.Confined;

			// mainMenuBackground.DOFade(1, 0);
			// background.DOFade(0, fadeDuration).onComplete += () =>
			// {
			// 	DOTween.KillAll();
			// 	SceneSwapper.i.SwapScene(InformationManager.Scene.MainMenu);
			// };
		};
	}

	private void RemoveImage()
	{
		DOTween.KillAll();
		ending.DOFade(0, fadeDuration).onComplete += GenerateFunFact;
	}

	private void GenerateFunFact()
	{
		selectedTextObject = funFacts[RandomNumberGenerator.GetInt32(0, funFacts.Count - 1)];
		selectedTextObject.SetActive(true);
		selectedText = selectedTextObject.GetComponent<TextMeshProUGUI>();
		selectedText.DOFade(1, fadeDuration).onComplete += () => textTweenComplete = true;
	}
}
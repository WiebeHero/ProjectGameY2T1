using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class FunFacts : MonoBehaviour
{
	[SerializeField] private GameObject funFactContainer;
	[SerializeField] private List<GameObject> funFacts;
	[SerializeField] private float fadeDuration;

	private Image background;

	private Random random;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)) Begin();
	}

	private void Start()
	{
		if (funFactContainer == null) 
			throw new Exception("FunFacts script doesn't know where the fun facts are :(");

		background = funFactContainer.GetComponentInChildren<Image>();
		if (background == null) throw new Exception("FunFacts can't find a background");
		
		EventHub.CarCrashStopEvent += Begin;
	}

	
	public void Begin()
	{
		funFactContainer.SetActive(true);
		
		
		funFacts[RandomNumberGenerator.GetInt32(0, funFacts.Count - 1)].SetActive(true);

		// Color targetColor = background.material.color;
		// targetColor.a = 255;

		background.DOFade(255, fadeDuration);

		//DOTween.To( () => background.material.color, x => background.material.color = x, targetColor, fadeDuration);
	}
}
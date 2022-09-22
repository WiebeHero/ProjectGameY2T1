using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Props
{
	public class PiranhaPlant : Interactable
	{
		[SerializeField] private GameObject plant;
		[SerializeField] private GameObject grownPlant;
		[SerializeField] private int necessaryClicks;
		
		private int amountClicked;
		private bool amountReached;
		private AudioSource audioSource;
		private Timer timer;
		
		private void Start()
		{
			timer = plant.AddComponent<Timer>();
			amountReached = false;
			amountClicked = 0;
			audioSource = GetComponent<AudioSource>();
		}

		protected override void OnLeftClick()
		{
			if (amountReached) return;
			
			amountClicked++;

			if (amountClicked == necessaryClicks)
			{
				StartCoroutine(PopUp());
			}
		}

		private IEnumerator PopUp()
		{
			amountReached = true;
			if (audioSource != null) audioSource.Play();
			
			yield return new WaitForSeconds(0.8f);
			
			plant.SetActive(false);
			grownPlant.SetActive(true);
		}

		protected override void OnLeftClickHold() {}
		protected override void OnLeftClickRelease(){}
		protected override void OnLookingAt() {}
		protected override void OnStopLookingAt(){}
	}
}
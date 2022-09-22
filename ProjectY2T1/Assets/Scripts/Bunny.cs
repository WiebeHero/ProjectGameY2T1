using System.Security.Cryptography;
using DG.Tweening;
using Managers;
using UnityEngine;

public class Bunny : MonoBehaviour
{
	private Tween tween;
	private AudioSource audioSource;
	private void Start()
	{
		if (RandomNumberGenerator.GetInt32(0, 7) != 6) return;

		audioSource = GetComponent<AudioSource>();
		
		tween = gameObject.transform.DOLocalRotate(new Vector3(0, 200, 0), 20);

		tween.onComplete += () =>
		{
			if (audioSource != null) 
				audioSource.Play();
		};

		EventHub.OnPauseEvent += () => tween.Pause();
		EventHub.OnContinueEvent += () => tween.Play();
	}
}
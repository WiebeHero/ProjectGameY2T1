using System.Security.Cryptography;
using DG.Tweening;
using UnityEngine;

public class Hector : Interactable
{
	[SerializeField] private AudioClip bell1, bell2;
	[SerializeField] private GameObject head;

	[SerializeField] private Vector3 originalRotation = new Vector3(-7.056f, 0, 0);
	[SerializeField] private Vector3 firstPosition = new Vector3(-30f, 0, 0);
	[SerializeField] private Vector3 secondPosition = new Vector3(12f, 0, 0);

	[Header("Timings")] 
	[SerializeField] private float firstDuration = 1.0f;
	[SerializeField] private float secondDuration = 1.7f;
	[SerializeField] private float thirdDuration = 0.2f;

	private AudioSource audioSource;
	private bool animationDone;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
		if (audioSource == null) Debug.LogWarning("Hector doesn't have an audioSource");

		audioSource.clip = bell1;
		animationDone = true;
	}


	protected override void OnLeftClick()
	{
		if (!audioSource.isPlaying)
		{
			audioSource.clip = RandomNumberGenerator.GetInt32(0, 2) == 0 ? bell1 : bell2;
			audioSource.Play();
		}

		if (!animationDone) return;
		animationDone = false;
		
		Transform headTransform = head.transform;

		headTransform.DOLocalRotate(firstPosition, firstDuration).onComplete += () => 
			headTransform.DOLocalRotate(secondPosition, secondDuration).onComplete += () => 
				headTransform.DOLocalRotate(originalRotation, thirdDuration).onComplete += () => animationDone = true;
	}
	
	protected override void OnLeftClickHold(){}
	protected override void OnLeftClickRelease(){}
	protected override void OnLookingAt(){}
	protected override void OnStopLookingAt(){}
}

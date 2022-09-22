using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public sealed class Phone : Interactable
{
	[Header("Zoom")] [SerializeField] private float startZoomDuration;
	[SerializeField] private float stopZoomDuration;
	[SerializeField] private float targetZoomFOV;
	
	[Header("Phone Audio")]
	[SerializeField] private AudioClip recieve, typing;
	private long timer, maxTimer;
	private bool myTurn;

	[Header("Animator")] [SerializeField] private Animator animator;

	[Header("Texts")] [SerializeField] private List<GameObject> texts;

	private float originalFOV;
	private int progress;
	private AudioSource source;

	private void Start()
	{
		maxTimer = 6500L;
		originalFOV = CameraController.i.cam.fieldOfView;
		timer = DateTimeOffset.Now.ToUnixTimeMilliseconds() + maxTimer;
		if (animator == null) throw new Exception("No animator found for the phone!");
		source = GetComponent<AudioSource>();
	}

	public void Update()
	{
		if (InformationManager.paused) timer += (long)(Time.deltaTime * 1000);
	}

	public void FixedUpdate()
	{
		if (progress < texts.Count && DateTimeOffset.Now.ToUnixTimeMilliseconds() >= timer && !myTurn && !InformationManager.isCrashing)
		{
			source.clip = recieve;
			source.Play();
			texts[progress].SetActive(true);
			animator.SetTrigger("Phone Progress");
			progress++;
			myTurn = true;
		}
	}

	protected override void OnLookingAt()
	{
		CameraController.i.Zoom(targetZoomFOV, startZoomDuration);
	}
	protected override void OnStopLookingAt()
	{
		CameraController.i.Zoom(originalFOV, stopZoomDuration);
	}

	protected override void OnLeftClick()
	{
		if (progress < texts.Count && myTurn)
		{
			source.clip = typing;
			source.Play();
			texts[progress].SetActive(true);
			animator.SetTrigger("Phone Progress");
			progress++;
			myTurn = false;
			timer = DateTimeOffset.Now.ToUnixTimeMilliseconds() + maxTimer;
		}
	}
	protected override void OnLeftClickHold() {}
	
	protected override void OnLeftClickRelease() {}

}
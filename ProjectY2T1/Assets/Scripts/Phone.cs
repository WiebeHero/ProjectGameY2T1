using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public sealed class Phone : Interactable
{
	[Header("Zoom")] [SerializeField] private float startZoomDuration;
	[SerializeField] private float stopZoomDuration;
	[SerializeField] private float targetZoomFOV;

	[Header("Animator")] [SerializeField] private Animator animator;

	[Header("Texts")] [SerializeField] private List<GameObject> texts;

	private float originalFOV;
	private int progress;

	private void Start()
	{
		originalFOV = CameraController.i.cam.fieldOfView;
		if (animator == null) throw new Exception("No animator found for the phone!");
	} 

	protected override void OnLookingAt() => CameraController.i.Zoom(targetZoomFOV, startZoomDuration);
	protected override void OnStopLookingAt() => CameraController.i.Zoom(originalFOV, stopZoomDuration);

	protected override void OnLeftClick()
	{
		if (progress < texts.Count)
		{
			texts[progress].SetActive(true);
			animator.SetTrigger("Phone Progress");
			progress++;
		}
	}
	protected override void OnLeftClickHold() {}
	
	protected override void OnLeftClickRelease() {}

}
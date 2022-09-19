using UnityEngine;

public sealed class Phone : Interactable
{
	[Header("Zoom")] [SerializeField] private float startZoomDuration;
	[SerializeField] private float stopZoomDuration;
	[SerializeField] private float targetZoomFOV;

	private float originalFOV;

	private void Start() => originalFOV = CameraController.i.cam.fieldOfView;

	protected override void OnLookingAt() => CameraController.i.Zoom(targetZoomFOV, startZoomDuration);
	protected override void OnStopLookingAt() => CameraController.i.Zoom(originalFOV, stopZoomDuration);
	protected override void OnLeftClick() {}
	protected override void OnLeftClickHold() {}
	
	protected override void OnLeftClickRelease() {}
}
using System;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
	public event Action LeftClick, LeftClickHold, LeftClickRelease, StartLookingAt, StopLookingAt;
	protected Interactable()
	{
		LeftClick += OnLeftClick;
		LeftClickHold += OnLeftClickHold;
		StartLookingAt += OnLookingAt;
		StopLookingAt += OnStopLookingAt;
		LeftClickRelease += OnLeftClickRelease;
	}

	public void OnLMB() => LeftClick?.Invoke();
	public void OnLMBHold() => LeftClickHold?.Invoke();
	public void OnLMBRelease() => LeftClickRelease?.Invoke();
	public void OnLookAt() => StartLookingAt?.Invoke();
	public void OnStopLookAt() => StopLookingAt?.Invoke();

	protected abstract void OnLeftClick();
	protected abstract void OnLeftClickHold();
	protected abstract void OnLeftClickRelease();
	protected abstract void OnLookingAt();
	protected abstract void OnStopLookingAt();
}
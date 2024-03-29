﻿using System;
using System.Collections;
using UnityEngine;


public sealed class Timer : MonoBehaviour
{
	[Tooltip("Duration of timer in seconds")]
	[SerializeField] 
	private int duration;
	
	public event Action Finished;

	public Timer(int duration_)
	{
		duration = duration_;
	}
	
	public void Run() => StartCoroutine(Time());
	
	private IEnumerator Time()
	{
		yield return new WaitForSeconds(duration);
		Finished?.Invoke();
	}
}
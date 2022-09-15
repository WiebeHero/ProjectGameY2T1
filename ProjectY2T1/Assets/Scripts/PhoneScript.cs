﻿using Managers;
using UnityEngine;

public sealed class PhoneScript : MonoBehaviour
{
	private GameObject notification;
	
	private void Start()
	{
		notification = transform.Find("Notification").gameObject;

		EventHub.StartedLookingBackwardsEvent += ShowNotification;
		EventHub.StopLookingAt += HideNotification;
	}

	private void ShowNotification()
	{
		if (EventTracker.it.eventCount[EventHub.CustomEvent.StartedLookingBackwards] < 1) notification.SetActive(true);
		//Play notification sound here
	} 

	private void HideNotification() => notification.SetActive(false);
}
﻿using Managers;
using UnityEngine;

public class PhoneScript : MonoBehaviour
{
	private GameObject notification;
	
	private void Start()
	{
		notification = transform.Find("Notification").gameObject;

		EventHub.StartedLookingBackwardsEvent += ShowNotification;
		EventHub.StoppedLookingAtPhoneEvent += HideNotification;
	}

	private void ShowNotification()
	{
		//Play notification sound here
		notification.SetActive(true);	
	} 

	private void HideNotification() => notification.SetActive(false);
}
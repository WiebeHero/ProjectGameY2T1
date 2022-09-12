using Managers;
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
		if (EventTracker.it.eventCount[EventHub.CustomEvent.StartedLookingBackwards] < 1) notification.SetActive(true);
		//Play notification sound here
	} 

	private void HideNotification() => notification.SetActive(false);
}
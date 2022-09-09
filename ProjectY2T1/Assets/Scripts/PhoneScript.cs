using Managers;
using UnityEngine;

public class PhoneScript : MonoBehaviour
{
	private GameObject notification;
	
	private void Start()
	{
		notification = transform.Find("Notification").gameObject;

		ManagerOfEvents.instance.StartedLookingBackwardsEvent += ShowNotification;
		ManagerOfEvents.instance.StoppedLookingAtPhoneEvent += HideNotification;
	}

	private void ShowNotification()
	{
		Debug.Log("jawel");
		//Play notification sound here
		notification.SetActive(true);	
	} 

	private void HideNotification() => notification.SetActive(false);
}
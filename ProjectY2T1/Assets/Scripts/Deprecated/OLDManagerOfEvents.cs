using System;
using UnityEngine;

// ReSharper disable EventNeverSubscribedTo.Global

namespace Deprecated
{
	// ReSharper disable once InconsistentNaming
	public class OLDManagerOfEvents : MonoBehaviour
	{
		public event Action StartedLookingBackwardsEvent;
		public event Action StoppedLookingBackwardsEvent;
		public event Action StartedLookingAtPhoneEvent;
		public event Action StoppedLookingAtPhoneEvent;
		
		public static OLDManagerOfEvents instance { get; private set; }
		private void Awake()
		{
			if (instance == null) instance = this;
			else Destroy(gameObject);
		}

		
		public enum CustomEvent
		{
			StartedLookingBackwards,
			StoppedLookingBackwards,
			StartedLookingAtPhone,
			StoppedLookingAtPhone
		}
		
		public void TriggerEvent(CustomEvent customEventName)
		{
			switch (customEventName)
			{
				case CustomEvent.StartedLookingBackwards:
					StartedLookingBackwardsEvent?.Invoke();
					break;
				case CustomEvent.StoppedLookingBackwards:
					StoppedLookingBackwardsEvent?.Invoke();
					break;
				case CustomEvent.StartedLookingAtPhone:
					StartedLookingAtPhoneEvent?.Invoke();
					break;
				case CustomEvent.StoppedLookingAtPhone:
					StoppedLookingAtPhoneEvent?.Invoke();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(customEventName), customEventName, null);
			}
		}
	}
}

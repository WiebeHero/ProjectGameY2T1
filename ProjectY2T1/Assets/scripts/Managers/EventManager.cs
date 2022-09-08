using System;
using UnityEngine;
// ReSharper disable EventNeverSubscribedTo.Global

namespace Managers
{
	public class EventManager : MonoBehaviour
	{
		
		
		public event Action PhoneOpenEvent;
		public event Action TestButtonPressEvent;
		public event Action StartedLookingBackwardsEvent;
		public event Action StoppedLookingBackwardsEvent;
		
		public static EventManager instance { get; private set; }
		private void Awake()
		{
			if (instance == null) instance = this;
			else Destroy(gameObject);
		}

		
		public enum CustomEvent
		{
			PhoneOpen,
			TestButtonPressed,
			StartedLookingBackwards,
			StoppedLookingBackwards,
		}
		
		public void TriggerEvent(CustomEvent customEventName)
		{
			switch (customEventName)
			{
				case CustomEvent.PhoneOpen:
					PhoneOpenEvent?.Invoke();
					break;
				case CustomEvent.TestButtonPressed:
					TestButtonPressEvent?.Invoke();
					break;
				case CustomEvent.StartedLookingBackwards:
					StartedLookingBackwardsEvent?.Invoke();
					break;
				case CustomEvent.StoppedLookingBackwards:
					StoppedLookingBackwardsEvent?.Invoke();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(customEventName), customEventName, null);
			}
		}
	}
}

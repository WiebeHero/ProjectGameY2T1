using System;
using UnityEngine;
// ReSharper disable EventNeverSubscribedTo.Global

namespace Managers
{
	public class EventManager : MonoBehaviour
	{
		
		
		public event Action PhoneOpenEvent;
		public event Action TestButtonPressEvent;
		
		public static EventManager instance { get; private set; }
		private void Awake()
		{
			if (instance == null) instance = this;
			else Destroy(gameObject);
		}

		
		public enum Event
		{
			PhoneOpen,
			TestButtonPressed
		}
		
		public void TriggerEvent(Event eventName)
		{
			switch (eventName)
			{
				case Event.PhoneOpen:
					PhoneOpenEvent?.Invoke();
					break;
				case Event.TestButtonPressed:
					TestButtonPressEvent?.Invoke();
					break;
			}
		}
	}
}

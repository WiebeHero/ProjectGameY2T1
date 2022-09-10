using System;
using System.Collections.Generic;

// ReSharper disable EventNeverSubscribedTo.Global

namespace Managers
{
	public static class ManagerOfEvents 
	{
		public static event Action StartedLookingBackwardsEvent;
		public static event Action StoppedLookingBackwardsEvent;
		public static event Action StartedLookingAtPhoneEvent;
		public static event Action StoppedLookingAtPhoneEvent;

		public static readonly Dictionary<CustomEvent, int> EventTriggerCount = new();

		private static bool initialized;

		public enum CustomEvent
		{
			StartedLookingBackwards,
			StoppedLookingBackwards,
			StartedLookingAtPhone,
			StoppedLookingAtPhone
		}
		
		public static void TriggerEvent(CustomEvent customEventName)
		{
			if (!initialized)
			{
				foreach (CustomEvent customEvent in Enum.GetValues(typeof(CustomEvent))) 
					EventTriggerCount.Add(customEvent, 0);
				initialized = true;
			}
			
			switch (customEventName)
			{
				case CustomEvent.StartedLookingBackwards:
					EventTriggerCount[CustomEvent.StartedLookingBackwards]++;
					StartedLookingBackwardsEvent?.Invoke();
					break;
				case CustomEvent.StoppedLookingBackwards:
					EventTriggerCount[CustomEvent.StoppedLookingBackwards]++;
					StoppedLookingBackwardsEvent?.Invoke();
					break;
				case CustomEvent.StartedLookingAtPhone:
					EventTriggerCount[CustomEvent.StartedLookingAtPhone]++;
					StartedLookingAtPhoneEvent?.Invoke();
					break;
				case CustomEvent.StoppedLookingAtPhone:
					EventTriggerCount[CustomEvent.StoppedLookingAtPhone]++;
					StoppedLookingAtPhoneEvent?.Invoke();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(customEventName), customEventName, null);
			}
		}
	}
}
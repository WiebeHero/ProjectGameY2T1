using System;
using static Managers.EventHub.CustomEvent;

// ReSharper disable EventNeverSubscribedTo.Global

namespace Managers
{
	public static class EventHub 
	{
		public static event Action StartedLookingBackwardsEvent;
		public static event Action StoppedLookingBackwardsEvent;
		public static event Action StartedLookingAtPhoneEvent;
		public static event Action StoppedLookingAtPhoneEvent;

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
			switch (customEventName)
			{
				case StartedLookingBackwards:
					StartedLookingBackwardsEvent?.Invoke();
					InteractionTracker.it.RecordEvent(StartedLookingBackwards);
					break;
				case StoppedLookingBackwards:
					StoppedLookingBackwardsEvent?.Invoke();
					InteractionTracker.it.RecordEvent(StoppedLookingBackwards);
					break;
				case StartedLookingAtPhone:
					StartedLookingAtPhoneEvent?.Invoke();
					InteractionTracker.it.RecordEvent(StartedLookingAtPhone);
					break;
				case StoppedLookingAtPhone:
					StoppedLookingAtPhoneEvent?.Invoke();
					InteractionTracker.it.RecordEvent(StoppedLookingAtPhone);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(customEventName), customEventName, null);
			}
		}
	}
}
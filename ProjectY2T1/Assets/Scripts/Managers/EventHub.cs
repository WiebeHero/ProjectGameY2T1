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
		public static event Action StartedLookingAtGasPedalEvent;
		public static event Action StoppedLookingAtGasPedalEvent;
		public static event Action SignAppearsEvent;

		private static bool initialized;

		public enum CustomEvent
		{
			StartedLookingBackwards, 
			StoppedLookingBackwards,
			StartedLookingAtPhone,
			StoppedLookingAtPhone,
			StartedLookingAtGasPedal,
			StoppedLookingAtGasPedal,
			SignAppears
		}
		
		public static void TriggerEvent(CustomEvent customEventName)
		{
			if (EventTracker.it == null) throw new Exception("No EventTracker in scene");
			
			switch (customEventName)
			{
				case StartedLookingBackwards:
					StartedLookingBackwardsEvent?.Invoke();
					EventTracker.it.RecordEvent(StartedLookingBackwards);
					break;
				case StoppedLookingBackwards:
					StoppedLookingBackwardsEvent?.Invoke();
						EventTracker.it.RecordEvent(StoppedLookingBackwards);
					break;
				case StartedLookingAtPhone:
					StartedLookingAtPhoneEvent?.Invoke();
					EventTracker.it.RecordEvent(StartedLookingAtPhone);
					break;
				case StoppedLookingAtPhone:
					StoppedLookingAtPhoneEvent?.Invoke();
					EventTracker.it.RecordEvent(StoppedLookingAtPhone);
					break;
				case StartedLookingAtGasPedal:
					StartedLookingAtGasPedalEvent?.Invoke();
					EventTracker.it.RecordEvent(StartedLookingAtGasPedal);
					break;
				case StoppedLookingAtGasPedal:
					StoppedLookingAtGasPedalEvent?.Invoke();
					EventTracker.it.RecordEvent(StoppedLookingAtGasPedal);
					break;
				case SignAppears:
					SignAppearsEvent?.Invoke();
					EventTracker.it.RecordEvent(SignAppears);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(customEventName), customEventName, null);
			}
		}
	}
}
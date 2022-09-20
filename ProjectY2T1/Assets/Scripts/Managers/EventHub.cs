using System;
using static Managers.EventHub.CustomEvent;

// ReSharper disable EventNeverSubscribedTo.Global

namespace Managers
{
	public static class EventHub
	{
		public static event Action StartedLookingBackwardsEvent;
		public static event Action StoppedLookingBackwardsEvent;
		public static event Action SignAppearsEvent;
		public static event Action StartLookingAt;
		public static event Action StopLookingAt;
		public static event Action CarCrashStartEvent;
		public static event Action CarCrashStopEvent;

		private static bool initialized;

		public enum CustomEvent
		{
			StartedLookingBackwards, 
			StoppedLookingBackwards,
			StartedLookingAt,
			StoppedLookingAt,
			SignAppears,
			CrashStartEvent,
			CrashStopEvent,
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
				case StartedLookingAt:
					StartLookingAt?.Invoke();
					EventTracker.it.RecordEvent(StartedLookingAt);
					break;
				case StoppedLookingAt:
					StopLookingAt?.Invoke();
					EventTracker.it.RecordEvent(StoppedLookingAt);
					break;
				case SignAppears:
					SignAppearsEvent?.Invoke();
					EventTracker.it.RecordEvent(SignAppears);
					break;
				case CrashStartEvent:
					CarCrashStartEvent?.Invoke();
					EventTracker.it.RecordEvent(SignAppears);
					break;
				case CrashStopEvent:
					CarCrashStopEvent?.Invoke();
					EventTracker.it.RecordEvent(SignAppears);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(customEventName), customEventName, null);
			}
		}
	}
}
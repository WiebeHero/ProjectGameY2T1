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
		public static event Action PointerEnterButtonEvent;
		public static event Action PointerExitButtonEvent;
		public static event Action OnPauseEvent;
		public static event Action OnContinueEvent;
		

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
			PointerEnterButton,
			PointerExitButton,
			OnPause,
			OnContinue
		}
		
		public static void TriggerEvent(CustomEvent customEventName)
		{
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
					EventTracker.it.RecordEvent(CrashStartEvent);
					break;
				case CrashStopEvent:
					CarCrashStopEvent?.Invoke();
					EventTracker.it.RecordEvent(CrashStopEvent);
					break;
				case PointerEnterButton:
					PointerEnterButtonEvent?.Invoke();
					break;
				case PointerExitButton:
					PointerExitButtonEvent?.Invoke();
					break;
				case OnPause:
					OnPauseEvent?.Invoke();
					break;
				case OnContinue:
					OnContinueEvent?.Invoke();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(customEventName), customEventName, null);
			}
		}
	}
}
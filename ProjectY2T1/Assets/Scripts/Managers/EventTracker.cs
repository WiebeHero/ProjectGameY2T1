using System;
using System.Collections.Generic;
using UnityEngine;
using static Managers.EventHub;

namespace Managers
{
	public class EventTracker : MonoBehaviour
	{
		public static EventTracker it;
		
		[NonSerialized] public string interactionSequence = "";
		[NonSerialized] public readonly Dictionary<CustomEvent, int> eventCount = new();

		public void Awake()
		{
			if (it != null && it != this)
			{
				Destroy(it);
				it = this;
			}
			else it = this;
			
			foreach (CustomEvent customEvent in Enum.GetValues(typeof(CustomEvent))) 
				eventCount.Add(customEvent, 0);
		}

		public void RecordEvent(CustomEvent customEvent)
		{
			interactionSequence += (int) customEvent;
			eventCount[customEvent]++;
		}

		public void ResetEventCount()
		{
			foreach (CustomEvent customEvent in Enum.GetValues(typeof(CustomEvent)))
				eventCount[customEvent] = 0;
		}


		public static string GenerateSequenceKey(List<CustomEvent> sequence)
		{
			string result = "";

			foreach (CustomEvent customEvent in sequence) 
				result += (int) customEvent;

			return result;
		}
	}
}
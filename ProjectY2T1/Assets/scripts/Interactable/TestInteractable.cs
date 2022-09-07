using Managers;

namespace Interactable
{
	public class TestInteractable : Interactable
	{
		protected override void OnInteract()
		{
			EventManager.instance.TriggerEvent(EventManager.Event.TestButtonPressed);
		}
	}
}

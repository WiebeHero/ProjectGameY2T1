using Managers;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseAlert : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public void OnPointerEnter(PointerEventData eventData) => 
		EventHub.TriggerEvent(EventHub.CustomEvent.PointerEnterButton);

	public void OnPointerExit(PointerEventData eventData) => 
		EventHub.TriggerEvent(EventHub.CustomEvent.PointerExitButton);
}
using System;
using UnityEngine;

namespace Interactable
{
	public abstract class Interactable : MonoBehaviour
	{
		public event Action InteractEvent;
		protected Interactable() => InteractEvent += OnInteract;

		public void Interact() => InteractEvent?.Invoke();

		protected abstract void OnInteract();
	}
}
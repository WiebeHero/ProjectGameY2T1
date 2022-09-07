using System;
using UnityEngine;

namespace Interactable
{
	public abstract class Interactable : MonoBehaviour
	{
		public event Action OnInteract;
		protected Interactable() => OnInteract += Execute;

		public void Interact() => OnInteract?.Invoke();

		protected abstract void Execute();
	}
}
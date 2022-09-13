using System;
using UnityEngine;

namespace Interactable
{
	public abstract class Interactable : MonoBehaviour
	{
		public event Action LeftClick, LeftClickHold;
		protected Interactable()
		{
			LeftClick += OnLeftClick;
			LeftClickHold += OnLeftClickHold;
		}

		public void OnLeft() => LeftClick?.Invoke();
		public void OnLeftHold() => LeftClickHold?.Invoke();

		protected abstract void OnLeftClick();
		protected abstract void OnLeftClickHold();
	}
}
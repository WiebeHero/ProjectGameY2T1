using UnityEngine;

namespace Props
{
	public class PiranhaPlant : Interactable
	{
		[SerializeField] private GameObject plant;
		[SerializeField] private GameObject grownPlant;
		[SerializeField] private int necessaryClicks;
		
		private int amountClicked;
		private bool amountReached;

		private void Start()
		{
			amountReached = false;
			amountClicked = 0;
		}

		protected override void OnLeftClick()
		{
			if (amountReached) return;
			
			amountClicked++;

			if (amountClicked == necessaryClicks)
			{
				amountReached = true;
				plant.SetActive(false);
				grownPlant.SetActive(true);
			}
		}

		protected override void OnLeftClickHold() {}
		protected override void OnLeftClickRelease(){}
		protected override void OnLookingAt() {}
		protected override void OnStopLookingAt(){}
	}
}
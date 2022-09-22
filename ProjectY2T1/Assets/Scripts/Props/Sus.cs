using DG.Tweening;
using UnityEngine;

namespace Props
{
	public class Sus : Interactable
	{
		//Original pos: (0, 0.1f, 0)
		//New pos: (0, -0.05f, 0) 
		[SerializeField] private GameObject sus;
		
		[Header("PositionalInformation")]
		[SerializeField] private float originalY = 0.1f;
		[SerializeField] private float newY = -0.05f;
		[SerializeField] private Vector3 originalRotation = new Vector3(0,145,180);
		[SerializeField] private Vector3 newRotation = new Vector3(0, 50, 180);
		
		[Header("Variables for tweaking")]
		[SerializeField] private int necessaryClicks;
		[SerializeField] private float ventDuration = 2f;
		[SerializeField] private float rotationDuration = 2f;
		private int timesClicked;
		private bool active;

		private void Start()
		{
			if (sus == null) 
				Debug.LogWarning("Sus doesn't work because the script doesn't know where sus is");
		}
		
		protected override void OnLeftClick()
		{
			if (active) return;

			timesClicked++;
			if (timesClicked == necessaryClicks)
			{
				Activate();
				timesClicked = 0;
			}
		}

		private void Activate()
		{
			active = true;

			Transform susTransform = sus.transform;

			susTransform.DOLocalMoveY(newY, ventDuration)
				.onComplete += () =>
				
				susTransform.DOLocalRotate(newRotation, rotationDuration)
					.onComplete += () =>
					
					susTransform.DOLocalRotate(originalRotation, rotationDuration)
						.onComplete += () =>
						
						susTransform.DOLocalMoveY(originalY, ventDuration)
							.onComplete += () => active = false;
		}
		
		protected override void OnLeftClickHold(){}
		protected override void OnLeftClickRelease(){}
		protected override void OnLookingAt(){}
		protected override void OnStopLookingAt() {}
	}
}
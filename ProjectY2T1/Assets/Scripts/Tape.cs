using System;
using UnityEngine;

public class Tape : Interactable.Interactable
{
	public AudioSource audioSource;
	private CassettePlayer cassettePlayer;

	private void Start()
	{
		if (cassettePlayer == null) 
			throw new Exception($"No Cassette Player assigned to tape");

		if (audioSource == null)
			throw new Exception("Tape has no AudioSource");
	}

	protected override void OnLookingAt()
	{
		
	}

	protected override void OnStopLookingAt()
	{
		
	}

	protected override void OnLeftClick()
	{
		cassettePlayer.PlayTape(this);
		gameObject.SetActive(false);
	}

	protected override void OnLeftClickHold() {}
}
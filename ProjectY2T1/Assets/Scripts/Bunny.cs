using System.Security.Cryptography;
using DG.Tweening;
using Managers;
using UnityEngine;

public class Bunny : MonoBehaviour
{
	private Tween tween;
	private void Start()
	{
		if (RandomNumberGenerator.GetInt32(0, 7) != 6) return;
		
		tween = gameObject.transform.DOLocalRotate(new Vector3(0,180,0), 30);

		EventHub.OnPauseEvent += () => tween.Pause();
		EventHub.OnContinueEvent += () => tween.Play();
	}
}
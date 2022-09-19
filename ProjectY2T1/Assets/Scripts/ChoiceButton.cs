﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public sealed class ChoiceButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
	[SerializeField] private GameObject spotLight;

	private Button button;

	private void Awake()
	{
		button = GetComponent<Button>();
		if (button == null) throw new Exception("How can a choice button not have a button!!!!????");
		
		if (spotLight == null) throw new Exception("No spotlight attached to this choice button");
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		spotLight.SetActive(true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		spotLight.SetActive(false);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		spotLight.SetActive(false);
	}
}
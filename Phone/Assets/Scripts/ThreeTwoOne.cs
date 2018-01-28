using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ThreeTwoOne : MonoBehaviour 
{
//	[SerializeField] int timeStart = 3;
	Text threeText;
	MessageManager receiveMessage;

	void Start () 
	{
		threeText = GetComponent<Text> ();
		receiveMessage = FindObjectOfType<MessageManager> ();
		FindObjectOfType<PlayButton> ().onTouchPlay += LauchCountdown;
	}
	
	public void LauchCountdown () 
	{
		Sequence countdownSequence = DOTween.Sequence ();
		threeText.text = "3";
		countdownSequence.Append (transform.DOScale (1.5f, 1f).SetEase(Ease.OutCubic).OnComplete(() => {threeText.text = "2"; transform.localScale = Vector3.one;}));
		countdownSequence.Append (transform.DOScale (1.5f, 1f).SetEase(Ease.OutCubic).OnComplete(() => {threeText.text = "1"; transform.localScale = Vector3.one;}));
		countdownSequence.Append (transform.DOScale (1.5f, 1f).SetEase(Ease.OutCubic).OnComplete(() => {threeText.text = "Go !"; transform.localScale = Vector3.one;}));
		countdownSequence.Append (transform.DOScale (1.5f, 1f).SetEase(Ease.OutCubic).OnComplete(() => {threeText.text = ""; transform.localScale = Vector3.one; receiveMessage.StartGame();}));
	}
}

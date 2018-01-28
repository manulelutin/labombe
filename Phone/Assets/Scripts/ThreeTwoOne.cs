using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using DG.Tweening;

public class ThreeTwoOne : MonoBehaviour 
{
//	[SerializeField] int timeStart = 3;
	Text threeText;
	MessageManager receiveMessage;
	AudioSource source;

	[SerializeField] AudioClip three;
	[SerializeField] AudioClip two;
	[SerializeField] AudioClip one;
	[SerializeField] AudioClip go;

	void Start () 
	{
		threeText = GetComponent<Text> ();
		source = GetComponent<AudioSource> ();
		receiveMessage = FindObjectOfType<MessageManager> ();
		FindObjectOfType<PlayButton> ().onTouchPlay += LauchCountdown;
	}
	
	public void LauchCountdown () 
	{
		source.PlayOneShot(three);
		Sequence countdownSequence = DOTween.Sequence ();
		threeText.text = "3";
		countdownSequence.Append (transform.DOScale (1.5f, 1f).SetEase(Ease.OutCubic).OnComplete(() => {threeText.text = "2"; transform.localScale = Vector3.one; source.PlayOneShot(two);}));
		countdownSequence.Append (transform.DOScale (1.5f, 1f).SetEase(Ease.OutCubic).OnComplete(() => {threeText.text = "1"; transform.localScale = Vector3.one; source.PlayOneShot(one);}));
		countdownSequence.Append (transform.DOScale (1.5f, 1f).SetEase(Ease.OutCubic).OnComplete(() => {threeText.text = "Go !"; transform.localScale = Vector3.one; source.PlayOneShot(go);}));
		countdownSequence.Append (transform.DOScale (1.5f, 1f).SetEase(Ease.OutCubic).OnComplete(() => {threeText.text = ""; transform.localScale = Vector3.one; receiveMessage.StartGame();}));
	}
}

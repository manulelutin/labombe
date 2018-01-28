using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using DG.Tweening;

public class Countdown : MonoBehaviour 
{
	[SerializeField] int time = 90;
	Text countdownText;
	Coroutine countdownCor;
	AudioSource countdownSource;

	void Start () 
	{
		countdownText = GetComponent<Text> ();
		MessageManager messageManager = FindObjectOfType<MessageManager> ();
		messageManager.onStartGame += StartCountdown;
		messageManager.onWin += ClearText;
		messageManager.onLose += ClearText;
	}

	void StartCountdown()
	{
		countdownCor = StartCoroutine (TimePass ());
	}
	
	IEnumerator TimePass()
	{
		countdownText.text = time.ToString () + "s";
		while(true)
		{
			yield return new WaitForSeconds (1f);
			time--;
			transform.DOPunchScale (Vector3.one * 0.2f, 0.2f, 8, 1);
			time = Mathf.Clamp(time, 0, 3000);
			countdownText.text = time.ToString () + "s";
		}
	}

	void ClearText()
	{
		StopCoroutine (countdownCor);
		countdownText.text = "";
	}
}

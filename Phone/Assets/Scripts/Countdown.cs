using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using DG.Tweening;

public class Countdown : MonoBehaviour 
{
	int time;
	[SerializeField] int timeInit = 90;
	[SerializeField] AudioClip[] tickSound;
	[SerializeField] AudioClip tickAlarmSound;
	[SerializeField] AudioClip[] countdownSound;
	Text countdownText;
	Coroutine countdownCor;
	AudioSource tickSource;
	[SerializeField] AudioSource countdownSource;

	void Start () 
	{
		countdownText = GetComponent<Text> ();
		tickSource = GetComponent<AudioSource> ();
		MessageManager messageManager = FindObjectOfType<MessageManager> ();
		messageManager.onStartGame += StartCountdown;
		messageManager.onWin += ClearText;
		messageManager.onLose += ClearText;

		time = timeInit;
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

			if(time > 20)
			{
				tickSource.PlayOneShot (tickSound [Random.Range (0, 2)]);
			}
			else if(time >= 0)
			{
				tickSource.PlayOneShot (tickAlarmSound);
			}

			if(time <= 10 && time > 0)
			{
				countdownSource.PlayOneShot (countdownSound[time - 1]);
			}
		}
	}

	void SyncTime(int newTime)
	{
		if(countdownCor != null)
		{
			StopCoroutine (countdownCor);
			countdownCor = null;
		}
		time = time;
		countdownText.text = time.ToString () + "s";
		countdownCor = StartCoroutine (TimePass ());
	}

	void ClearText()
	{
		StopCoroutine (countdownCor);
		countdownText.text = "";
		time = timeInit;
	}
}

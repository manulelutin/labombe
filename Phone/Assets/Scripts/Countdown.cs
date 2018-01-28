using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Countdown : MonoBehaviour 
{
	[SerializeField] int time = 90;
	Text countdownText;
	Coroutine countdownCor;

	void Start () 
	{
		countdownText = GetComponent<Text> ();
		FindObjectOfType<MessageManager> ().onStartGame += StartCountdown;
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
			countdownText.text = time.ToString () + "s";
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Audio;

public class EndScreen : MonoBehaviour 
{
	AudioSource winLoseSource;
	[SerializeField] AudioClip winClip;
	[SerializeField] AudioClip loseClip;

	[SerializeField] AudioSource secondSource;
	[SerializeField] AudioClip explosionClip;
	[SerializeField] AudioClip applauseClip;

	Text winLoseText;

	void Start () 
	{
		winLoseSource = GetComponent<AudioSource> ();
		winLoseText = GetComponentInChildren<Text> ();

		MessageManager messageManager = FindObjectOfType<MessageManager> ();
		messageManager.onWin += Win;
		messageManager.onLose += Lose;
		messageManager.onRestart += ClearText;
	}
	
	void Win () 
	{
		winLoseText.text = "You win !!";
		winLoseSource.PlayOneShot (winClip);
		secondSource.PlayOneShot (applauseClip);
		transform.DOScale (1.5f, 3f);
	}

	void Lose () 
	{
		winLoseText.text = "You lose...";
		secondSource.PlayOneShot (explosionClip);
		winLoseSource.PlayOneShot (loseClip);
		transform.DOScale (1.5f, 3f);
	}

	void ClearText()
	{
		winLoseText.text = "";
		transform.localScale = Vector3.one;
	}
}

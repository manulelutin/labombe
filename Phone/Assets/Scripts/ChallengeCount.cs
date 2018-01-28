using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChallengeCount : MonoBehaviour 
{
	[SerializeField] Image background;
	[SerializeField] Text challengeCountText;

	void Start () 
	{
		MessageManager messageManager = FindObjectOfType<MessageManager> ();
		messageManager.onStartGame += Show;
		messageManager.onWin += Hide;
		messageManager.onLose += Hide;
	}

	void Show()
	{
		background.enabled = true;
	}

	void Hide()
	{
		background.enabled = false;
	}
	
	void UpdateChallengeCount (int challengeLeft) 
	{
		challengeCountText.text = challengeLeft.ToString ();
		transform.DOPunchScale (Vector3.one * 0.1f, 0.5f, 8, 1);
	}
}

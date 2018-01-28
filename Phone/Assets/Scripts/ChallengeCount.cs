using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeCount : MonoBehaviour 
{
	[SerializeField] Image background;
	[SerializeField] Text challengeCountText;

	void Start () 
	{
		MessageManager messageManager = FindObjectOfType<MessageManager> ();
		messageManager.onStartGame += Show;
	}

	void Show()
	{
		background.enabled = true;
	}
	
	void UpdateChallengeCount (int challengeLeft) 
	{
		challengeCountText.text = challengeLeft.ToString ();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeCount : MonoBehaviour 
{
	[SerializeField] Image background;
	[SerializeField] Text challengeCountText;
	int challenges = 10;

	void Start () 
	{
		MessageManager messageManager = FindObjectOfType<MessageManager> ();
		messageManager.onStartGame += Show;
		messageManager.onStartGame += Show;
		challenges++;
	}

	void Show()
	{
		background.enabled = true;
	}
	
	void UpdateChallengeCount () 
	{
		challenges--;
		challengeCountText.text = challenges.ToString ();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeText : MonoBehaviour 
{
	Text challengeTitleText;

	void Awake () 
	{
		challengeTitleText = GetComponent<Text> ();
		MessageManager messageManager = FindObjectOfType<MessageManager> ();
		messageManager.onWin += ClearText;
		messageManager.onLose += ClearText;
	}

	public void UpdateTitle (string title) 
	{
		challengeTitleText.text = title;
	}

	void ClearText()
	{
		challengeTitleText.text = "";
	}
}

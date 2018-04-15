using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepeatChallenge : MonoBehaviour
{
	[SerializeField] Image buttonImage;
	[SerializeField] Text countText;

	void Start ()
	{
		MessageManager messageManager = FindObjectOfType<MessageManager> ();
		messageManager.onNewInstruction += Hide;
		messageManager.onNewRepeat += ShowRepeat;
	}
	
	void Hide ()
	{
		buttonImage.enabled = false;
		countText.text = "";
	}

	void ShowRepeat (string button, int count)
	{
		buttonImage.enabled = true;
		switch (button)
		{
		case "green":
			buttonImage.color = Color.green;
			break;
		case "blue":
			buttonImage.color = Color.blue;
			break;
		case "yellow":
			buttonImage.color = Color.yellow;
			break;
		case "red":
			buttonImage.color = Color.red;
			break;
		}

		countText.text = "x " + count.ToString ();
	}
}

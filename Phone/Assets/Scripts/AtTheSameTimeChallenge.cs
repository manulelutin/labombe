using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtTheSameTimeChallenge : MonoBehaviour
{
	[SerializeField] GameObject plus;
	[SerializeField] GameObject greenButton;
	[SerializeField] GameObject blueButton;
	[SerializeField] GameObject yellowButton;
	[SerializeField] GameObject redButton;

	void Start () 
	{
		MessageManager messageManager = FindObjectOfType<MessageManager> ();
		messageManager.onNewInstruction += Hide;
		messageManager.onNewAtTheSameTime += ShowButtons;
	}
	
	void Hide () 
	{
		plus.SetActive (false);
		greenButton.SetActive (false);
		blueButton.SetActive (false);
		yellowButton.SetActive (false);
		redButton.SetActive (false);
	}

	public void ShowButtons(string[] buttons)
	{
		plus.SetActive (true);
		List<string> newButtons = new List<string> ();
		for (int i = 0; i < buttons.Length; i++) {
			newButtons.Add (buttons [i]);
		}
		if(newButtons.Contains("green"))
			greenButton.SetActive (true);
		if(newButtons.Contains("blue"))
			blueButton.SetActive (true);
		if(newButtons.Contains("yellow"))
			yellowButton.SetActive (true);
		if(newButtons.Contains("red"))
			redButton.SetActive (true);
	}
}

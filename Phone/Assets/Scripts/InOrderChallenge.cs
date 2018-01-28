using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InOrderChallenge : MonoBehaviour 
{
	[SerializeField] GameObject[] circles;
	[SerializeField] GameObject[] arrows;

	void Start()
	{
		MessageManager messageManager = FindObjectOfType<MessageManager> ();
		messageManager.onNewInstruction += Hide;
		messageManager.onNewSequence += ShowSequence;
	}

	void Hide()
	{
		for (int i = 0; i < circles.Length; i++) 
		{
			circles [i].SetActive (false);
			if (arrows [i] != null)
				arrows [i].SetActive (false);
		}
	}

	public void ShowSequence (string[] sequence) 
	{

		for (int i = 0; i < sequence.Length; i++) 
		{
			circles [i].SetActive (true);
			switch (sequence[i]) 
			{
			case "red":
				circles [i].GetComponent<Image> ().color = Color.red;
				break;
			case "yellow":
				circles [i].GetComponent<Image> ().color = Color.yellow;
				break;
			case "green":
				circles [i].GetComponent<Image> ().color = Color.green;
				break;
			case "blue":
				circles [i].GetComponent<Image> ().color = Color.blue;
				break;
			}
			if (arrows [i] != null)
				arrows [i].SetActive (true);
		}
	}
}

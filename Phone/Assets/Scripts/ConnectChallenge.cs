using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectChallenge : MonoBehaviour 
{
	[SerializeField] Image[] ins;
	[SerializeField] GameObject up;
	[SerializeField] GameObject down;


	void Start()
	{
		MessageManager messageManager = FindObjectOfType<MessageManager> ();
		messageManager.onNewInstruction += Hide;
		messageManager.onNewConnect += ShowConnect;
	}

	void Hide()
	{
		up.SetActive (false);
		down.SetActive (false);
	}
	
	void ShowConnect (string[][] connects) 
	{
		up.SetActive (true);
		switch (connects[0][0]) {
		case "cablePurple":
			ins [0].color = new Color (1f, 0.05f, 1f);
			break;
		case "cableGrey":
			ins [0].color = Color.gray;
			break;
		case "cableOrange":
			ins [0].color = new Color (1f, 0.1f, 0.05f);
			break;
		case "cableBlack":
			ins [0].color = Color.black;
			break;
		case "cableWhite":
			ins [0].color = Color.white;
			break;
		}
		switch (connects[0][1]) {
		case "cablePurple":
			ins [1].color = new Color (1f, 0.05f, 1f);
			break;
		case "cableGrey":
			ins [1].color = Color.gray;
			break;
		case "cableOrange":
			ins [1].color = new Color (1f, 0.1f, 0.05f);
			break;
		case "cableBlack":
			ins [1].color = Color.black;
			break;
		case "cableWhite":
			ins [1].color = Color.white;
			break;
		}

		if(connects[1][0] != "")
		{
			down.SetActive (true);
			switch (connects[1][0]) {
			case "cablePurple":
				ins [2].color = new Color (1f, 0.05f, 1f);
				break;
			case "cableGrey":
				ins [2].color = Color.gray;
				break;
			case "cableOrange":
				ins [2].color = new Color (1f, 0.1f, 0.05f);
				break;
			case "cableBlack":
				ins [2].color = Color.black;
				break;
			case "cableWhite":
				ins [2].color = Color.white;
				break;
			}
			switch (connects[1][1]) {
			case "cablePurple":
				ins [3].color = new Color (1f, 0.05f, 1f);
				break;
			case "cableGrey":
				ins [3].color = Color.gray;
				break;
			case "cableOrange":
				ins [3].color = new Color (1f, 0.1f, 0.05f);
				break;
			case "cableBlack":
				ins [3].color = Color.black;
				break;
			case "cableWhite":
				ins [3].color = Color.white;
				break;
			}
		}
	}
}

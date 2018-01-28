using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleChallenge : MonoBehaviour {

	[SerializeField] GameObject[] levers;
	[SerializeField] GameObject[] arrows;

	[SerializeField] Sprite upSprite;
	[SerializeField] Sprite downSprite;
	[SerializeField] Sprite offSprite;

	void Start()
	{
		MessageManager messageManager = FindObjectOfType<MessageManager> ();
		messageManager.onNewInstruction += Hide;
		messageManager.onNewToggle += ShowToggle;
	}

	void Hide()
	{
		for (int i = 0; i < levers.Length; i++) 
		{
			levers [i].SetActive (false);
			if (arrows [i] != null)
				arrows [i].SetActive (false);
		}
	}

	void ShowToggle(string[] states)
	{
		for (int i = 0; i < levers.Length; i++) 
		{
			levers [i].SetActive (true);
			arrows [i].SetActive (true);
			switch (states[i]) 
			{
			case "up":
				arrows [i].GetComponent<Image> ().sprite = upSprite;
				break;
			case "down":
				arrows [i].GetComponent<Image> ().sprite = downSprite;
				break;
			case "off":
				arrows [i].GetComponent<Image> ().sprite = offSprite;
				break;
			default:
				break;
			}
		}
	}
}

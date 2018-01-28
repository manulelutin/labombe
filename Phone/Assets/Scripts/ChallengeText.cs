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
	}

	public void UpdateTitle (string title) 
	{
		challengeTitleText.text = title;
	}
}

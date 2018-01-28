using System;
using WebSocketSharp;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MessageManager : MonoBehaviour
{
	InOrderChallenge inOrderChallenge;
	ChallengeText challengeText;
	SoundController soundController;
	List<RaspberryInfos> messagesList = new List<RaspberryInfos>();
	WebSocket ws;

	public Action<int> onUpdateChallenge;
	public Action onNewInstruction;
	public Action<string[]> onNewSequence;
	public Action<string[]> onNewAtTheSameTime;
	public Action<string, int> onNewRepeat;
	public Action onStartGame;
	public Action onWin;
	public Action onLose;
	public Action onRestart;

	void Start ()
	{
		inOrderChallenge = FindObjectOfType<InOrderChallenge> ();
		challengeText = FindObjectOfType<ChallengeText> ();
		soundController = FindObjectOfType<SoundController> ();

		ws = new WebSocket ("ws://192.168.43.37");
		ws.OnMessage += (sender, e) =>	(NewMessage(e.Data));
		ws.OnError += (sender,  i) =>	(print ("Error "));
		ws.OnOpen += (sender, c) =>	(print ("Open "));
		ws.OnClose += (sender, j) =>	(print ("Close "));

		ws.Connect ();
	}

	void NewMessage(string json)
	{
		RaspberryInfos infos = RaspberryInfos.CreateFromJSON (json);
		messagesList.Add (infos);
	}

	void Update()
	{
		for (int i = 0; i < messagesList.Count; i++) 
		{
			if (onNewInstruction != null)
				onNewInstruction ();
			switch (messagesList[0].instruction) 
			{
			case "challengeStart":

				switch (messagesList[0].challengeType) {
				case "Sequence":
					if (onNewSequence != null)
						onNewSequence (messagesList[0].sequenceList);
					challengeText.UpdateTitle ("In order");
					break;
				case "AtTheSameTime":
					if (onNewAtTheSameTime != null)
						onNewAtTheSameTime (messagesList[0].selectedButton);
					challengeText.UpdateTitle ("At the same time");
					break;
				case "Repeat":
					if (onNewRepeat != null)
						onNewRepeat (messagesList[0].button, messagesList[0].count);
					challengeText.UpdateTitle ("Repeat !");
					break;
				}

				if (onUpdateChallenge != null)
					onUpdateChallenge (messagesList[0].challengeLeft);
				
				break;
			case "gameWin":
				Win ();
				break;
			case "gameLose":
				Lose ();
				break;
//			case "playSound":
//				soundController.Play ("");
//				break;
			default:
				break;
			}

			messagesList.RemoveAt (0);
		}
	}

	public void StartGame()
	{
		if (onStartGame != null)
			onStartGame ();
		RaspberryInfos startInfos = new RaspberryInfos ();
		startInfos.instruction = "startGame";
		ws.Send (JsonUtility.ToJson(startInfos));
	}

	public void Win()
	{

		if (onWin != null)
			onWin ();
		StartCoroutine (WaitRestart ());
	}

	public void Lose()
	{

		if (onLose != null)
			onLose ();
		StartCoroutine (WaitRestart ());
	}

	IEnumerator WaitRestart()
	{
		yield return new WaitForSeconds (5f);
		if (onRestart != null)
			onRestart ();
	}
}
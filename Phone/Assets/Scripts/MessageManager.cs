using System;
using WebSocketSharp;
using UnityEngine;
using System.Collections.Generic;

public class MessageManager : MonoBehaviour
{
	InOrderChallenge inOrderChallenge;
	ChallengeText challengeText;
	List<RaspberryInfos> messagesList = new List<RaspberryInfos>();
	WebSocket ws;

	public Action onNewChallenge;
	public Action onStartGame;

	void Start ()
	{
		inOrderChallenge = FindObjectOfType<InOrderChallenge> ();
		challengeText = FindObjectOfType<ChallengeText> ();

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
			switch (messagesList[0].challengeType) {
			case "Sequence":
				inOrderChallenge.ShowSequence (messagesList [i].sequenceList);
				challengeText.UpdateTitle ("In order");
				break;
			}

			if (onNewChallenge != null)
				onNewChallenge ();
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
		
	}

	public void Lose()
	{
		
	}
}
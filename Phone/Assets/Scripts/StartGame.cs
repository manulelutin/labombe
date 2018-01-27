using System;
using WebSocketSharp;
using UnityEngine;

public class StartGame : MonoBehaviour
{
//	MessageEventArgs i;
	void Start ()
	{
		WebSocket ws = new WebSocket ("ws://192.168.43.37");
		ws.OnMessage += (sender, e) =>	(NewMessage(e.Data));
		ws.OnError += (sender,  i) =>	(print ("Error "));
		ws.OnOpen += (sender, c) =>	(print ("Open "));
		ws.OnClose += (sender, j) =>	(print ("Close "));


		ws.Connect ();
		ws.Send ("BALUS");
//		Console.ReadKey (true);
	}

	void NewMessage(string json)
	{
		ReaspberryInfos infos = ReaspberryInfos.CreateFromJSON (json);

		Debug.Log (infos.instruction);
		Debug.Log (infos.challengeType);
		for (int i = 0; i < infos.sequenceList.Length; i++) {
			Debug.Log (infos.sequenceList[i]);
		}
		Debug.Log (infos.timeLeft);
		Debug.Log (infos.challengeLeft);
	}
}
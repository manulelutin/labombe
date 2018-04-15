using System;
using WebSocketSharp;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using UnityEngine.Networking;

public class MessageManager : MonoBehaviour
{
	InOrderChallenge inOrderChallenge;
	ChallengeText challengeText;
	SoundController soundController;
	Countdown countdown;
	List<RaspberryInfos> messagesList = new List<RaspberryInfos> ();
	WebSocket ws;

	AudioSource challengeSource;
	[SerializeField] AudioClip challengeClip;

	public Action<int> onUpdateChallenge;
	public Action<float> onUpdateTime;
	public Action onNewInstruction;
	public Action<string[]> onNewSequence;
	public Action<string[]> onNewAtTheSameTime;
	public Action<string, int> onNewRepeat;
	public Action<string[]> onNewToggle;
	public Action<string[][]> onNewConnect;
	public Action onStartGame;
	public Action onWin;
	public Action onLose;
	public Action onRestart;

	public string LocalIPAddress ()
	{
		IPHostEntry host;
		string localIP = "";
		host = Dns.GetHostEntry (Dns.GetHostName ());
		print ("GetHostName = " + Dns.GetHostName ());
		foreach (IPAddress ip in host.AddressList)
		{
			print (ip.ToString ());
			if (ip.AddressFamily == AddressFamily.InterNetwork)
			{
				localIP = ip.ToString ();
				break;
			}
		}
		return localIP;
	}

	void Start ()
	{
		inOrderChallenge = FindObjectOfType<InOrderChallenge> ();
		challengeText = FindObjectOfType<ChallengeText> ();
		soundController = FindObjectOfType<SoundController> ();
		challengeSource = FindObjectOfType<AudioSource> ();
		challengeSource = FindObjectOfType<AudioSource> ();
		countdown = FindObjectOfType<Countdown> ();

		ws = new WebSocket ("ws://192.168.43.37");
		print ("local ip = " + LocalIPAddress ());
//		GetComponent<NetworkDiscovery> ().Initialize ();
//		GetComponent<NetworkDiscovery> ().StartAsClient ();
//		GetComponent<NetworkDiscovery> ().OnReceivedBroadcast ("ws://192.168.43.37", );
		for (int i = 0; i < GetComponent<NetworkDiscovery> ().broadcastsReceived.Keys.Count; i++)
		{
			print (GetComponent<NetworkDiscovery> ().broadcastsReceived.Keys);
		}
		ws.OnMessage += (sender, e) =>	(NewMessage (e.Data));
		ws.OnError += (sender, i) =>	(print ("Error "));
		ws.OnOpen += (sender, c) =>	(print ("Open "));
		ws.OnClose += (sender, j) =>	(print ("Close "));

		ws.Connect ();
	}

	void NewMessage (string json)
	{
		RaspberryInfos infos = RaspberryInfos.CreateFromJSON (json);
		print (json);
		messagesList.Add (infos);
	}

	void Update ()
	{
		for (int i = 0; i < messagesList.Count; i++)
		{
			switch (messagesList [0].instruction)
			{
			case "challengeStart":
				if (onNewInstruction != null)
					onNewInstruction ();

				switch (messagesList [0].challengeType)
				{
				case "Sequence":
					if (onNewSequence != null)
						onNewSequence (messagesList [0].sequenceList);
					challengeText.UpdateTitle ("In order");
					break;
				case "AtTheSameTime":
					if (onNewAtTheSameTime != null)
						onNewAtTheSameTime (messagesList [0].selectedButton);
					challengeText.UpdateTitle ("At the same time");
					break;
				case "Repeat":
					if (onNewRepeat != null)
						onNewRepeat (messagesList [0].button, messagesList [0].count);
					challengeText.UpdateTitle ("Repeat !");
					break;
				case "Switch":
					if (onNewToggle != null)
						onNewToggle (messagesList [0].switchState);
					challengeText.UpdateTitle ("Toggle");
					break;
				case "Connect":
					if (onNewConnect != null)
						onNewConnect (messagesList [0].cablesConnection);
					challengeText.UpdateTitle ("Connect !");
					break;
				default:
					break;
				}

				challengeSource.PlayOneShot (challengeClip);
				
				break;
			case "gameWin":
				if (onNewInstruction != null)
					onNewInstruction ();
				Win ();
				break;
			case "gameLose":
				if (onNewInstruction != null)
					onNewInstruction ();
				Lose ();
				break;
			case "playSound":
				soundController.Play (messagesList [0].soundName);
				break;
			default:
				break;
			}

			if (messagesList [0].timeLeft != 0f && onUpdateTime != null)
			{
				onUpdateTime (messagesList [0].timeLeft);
				countdown.SyncTime ((int)messagesList [0].timeLeft);
			}
				

			if (messagesList [0].challengeLeft != 0 && onUpdateChallenge != null)
				onUpdateChallenge (messagesList [0].challengeLeft);

			messagesList.RemoveAt (0);
		}
	}

	public void StartGame ()
	{
		if (onStartGame != null)
			onStartGame ();
		RaspberryInfos startInfos = new RaspberryInfos ();
		startInfos.instruction = "startGame";
		ws.Send (JsonUtility.ToJson (startInfos));
	}

	public void Win ()
	{

		if (onWin != null)
			onWin ();
		StartCoroutine (WaitRestart ());
	}

	public void Lose ()
	{

		if (onLose != null)
			onLose ();
		StartCoroutine (WaitRestart ());
	}

	IEnumerator WaitRestart ()
	{
		yield return new WaitForSeconds (5f);
		if (onRestart != null)
			onRestart ();
	}
}
﻿using UnityEngine;

[System.Serializable]
public class RaspberryInfos
{
	public string instruction;
	public string challengeType;
	public string[] sequenceList;
	public string[] selectedButton;	
	public string button;	
	public int count;	
	public string[] switchState;	
	public string[] cablesConnection;	
	public int timeLeft;
	public int challengeLeft;
    public string soundName;

	public static RaspberryInfos CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<RaspberryInfos>(jsonString);
	}
}
using UnityEngine;

[System.Serializable]
public class RaspberryInfos
{
	public string instruction;
	public string challengeType;
	public string[] sequenceList;
	public int timeLeft;
	public int challengeLeft;

	public static RaspberryInfos CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<RaspberryInfos>(jsonString);
	}
}
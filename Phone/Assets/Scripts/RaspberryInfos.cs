using UnityEngine;

[System.Serializable]
public class ReaspberryInfos
{
	public string instruction;
	public string challengeType;
	public string[] sequenceList;
	public int timeLeft;
	public int challengeLeft;

	public static ReaspberryInfos CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<ReaspberryInfos>(jsonString);
	}
}
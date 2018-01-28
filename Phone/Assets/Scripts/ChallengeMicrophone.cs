using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneChallenge : MonoBehaviour {

    AudioClip microphoneInput;
    bool microphoneInitialized;
    public float sensitivity;
    public bool flapped;

    // Use this for initialization
    void Start () {
        //init microphone input
        if (Microphone.devices.Length > 0) {
            microphoneInput = Microphone.Start(Microphone.devices[0], true, 999, 44100);
            microphoneInitialized = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if()
    }
}

using UnityEngine;

public class SoundController : MonoBehaviour {
    [System.Serializable]
    public class SoundCue {
        public string name;
        public AudioClip clip;
    }

    public SoundCue[] cues;
    public AudioSource source;

	void Start()
	{
		source = GetComponent<AudioSource> ();
	}

    public void Play(string name) {
        foreach (SoundCue cue in cues) {
            if (cue.name == name) {
                source.PlayOneShot(cue.clip, 1);
            }
        }
    }

}

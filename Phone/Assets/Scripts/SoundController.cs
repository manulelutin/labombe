using UnityEngine;

public class SoundController : MonoBehaviour {
    [System.Serializable]
    public class SoundCue {
        public string name;
        public float timeout = 0.1f;
        private float lastPlayTime = 0;
        public void Play(AudioSource source) {
            float now = Time.time;
            if (now>= lastPlayTime + timeout) {
                lastPlayTime = now;
                source.PlayOneShot(clip, 1);
            }
        }
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
                cue.Play(source);
            }
        }
    }

}

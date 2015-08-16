using UnityEngine;
using System.Collections;

public class PauseControl : MonoBehaviour {

	float _storedTimeScale;

	// Use this for initialization
	void Start () {

		_storedTimeScale = Time.timeScale;

		Messenger.AddListener ("LevelEnding", Disable);

	}

	void Disable() {
		this.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Return)) {

			if (Time.timeScale > 0f) {
				Time.timeScale = 0f;
				Messenger.Broadcast("LevelPause");
			} else {
				Time.timeScale = _storedTimeScale;
				Messenger.Broadcast("LevelResume");
			}

		}
	
	}
}

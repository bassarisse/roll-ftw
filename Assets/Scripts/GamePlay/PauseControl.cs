using UnityEngine;
using System.Collections;

public class PauseControl : MonoBehaviour {
	
	public Fader fader;
	
	bool _paused;
	//float _storedTimeScale;

	// Use this for initialization
	void Start () {

		_paused = false;
		//_storedTimeScale = Time.timeScale;

		this.enabled = false;
		
		Messenger.AddListener ("LevelStart", Activate);
		Messenger.AddListener ("LevelEnding", Deactivate);

	}
	
	void Activate() {
		this.enabled = true;
	}
	
	void Deactivate() {
		this.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Return)) {

			_paused = !_paused;

			if (_paused) {
				//Time.timeScale = 0f;
				Messenger.Broadcast("LevelPause");
			} else {
				//Time.timeScale = _storedTimeScale;
				Messenger.Broadcast("LevelResume");
			}

		}

		if (!_paused)
			return;
		
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			if (fader == null) {
				RestartLevel();
			} else {
				fader.SetColor(new Color(1, 1, 1, 0));
				fader.Play(true, gameObject, "RestartLevel");
			}
			AudioHandler.Play("selection");
			this.enabled = false;
			return;
		}
		
		if (Input.GetKeyDown (KeyCode.DownArrow))
		{
			if (fader == null) {
				ReturnToTitleScreen();
			} else {
				fader.SetColor(new Color(0, 0, 0, 0));
				fader.Play(true, gameObject, "ReturnToTitleScreen");
			}
			AudioHandler.Play("selection");
			this.enabled = false;
			return;
		}
	
	}
	
	void RestartLevel() {
		GameState.LoadLevel();
	}
	
	void ReturnToTitleScreen() {
		Application.LoadLevel("TitleScreen");
	}

}

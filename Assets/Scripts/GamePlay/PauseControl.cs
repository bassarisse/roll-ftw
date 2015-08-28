using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseControl : BaseControl {
	
	bool _paused;
	//float _storedTimeScale;
	float _touchTimer;

	// Use this for initialization
	void Start () {

		_paused = false;
		//_storedTimeScale = Time.timeScale;
		_touchTimer = 0f;

		this.enabled = false;
		
		Messenger.AddListener ("LevelStart", Activate);
		Messenger.AddListener ("LevelEnding", Deactivate);

	}
	
	void Activate() {

		this.enabled = true;

		Messenger.AddListener ("Touch.Left", TriggerRestartLevel);
		Messenger.AddListener ("Touch.Down", TriggerReturnToTitleScreen);

	}
	
	void Deactivate() {

		this.enabled = false;

		Messenger.RemoveListener ("Touch.Left", TriggerRestartLevel);
		Messenger.RemoveListener ("Touch.Down", TriggerReturnToTitleScreen);

	}
	
	// Update is called once per frame
	void Update () {

		var hasThreeTouches = Input.touchCount >= 3;

		if (InputExtensions.Pressed.Start || (hasThreeTouches && _touchTimer <= 0f)) {

			_paused = !_paused;

			if (_paused) {
				//Time.timeScale = 0f;
				Messenger.Broadcast("LevelPause");
			} else {
				//Time.timeScale = _storedTimeScale;
				Messenger.Broadcast("LevelResume");
			}

		}

		if (hasThreeTouches) {
			_touchTimer = 0.15f;
		} else if (_touchTimer > 0f) {
			_touchTimer -= Time.deltaTime;
		}
		
		if (!_paused)
			return;
		
		if (InputExtensions.Pressed.Left)
		{
			TriggerRestartLevel();
			return;
		}
		
		if (InputExtensions.Pressed.Down)
		{
			TriggerReturnToTitleScreen();
			return;
		}
	
	}
	
	void TriggerRestartLevel() {
		if (!_paused || _touchTimer > 0f)
			return;
		Deactivate ();
		TriggerFade(RestartLevel, new Color(1, 1, 1, 0));
		AudioHandler.Play("selection");
		ArrowFeedback.Left();
	}
	
	void RestartLevel() {
		GameState.LoadLevel();
	}
	
	void TriggerReturnToTitleScreen() {
		if (!_paused || _touchTimer > 0f)
			return;
		Deactivate ();
		TriggerFade(ReturnToTitleScreen, new Color(0, 0, 0, 0));
		AudioHandler.Play("selection");
		ArrowFeedback.Down();
	}
	
	void ReturnToTitleScreen() {
		Application.LoadLevel("TitleScreen");
	}

}

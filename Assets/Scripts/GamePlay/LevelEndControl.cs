using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelEndControl : BaseControl {

	void Start() {
		
		AudioHandler.Load ("selection");
		
		Messenger.AddListener ("Touch.Up", TriggerReturnToTitleScreen);
		Messenger.AddListener ("Touch.Left", TriggerRestartLevel);
		Messenger.AddListener ("Touch.Right", TriggerNextLevel);

	}
	
	void Disable() {
		
		this.enabled = false;
		
		Messenger.RemoveListener ("Touch.Up", TriggerReturnToTitleScreen);
		Messenger.RemoveListener ("Touch.Left", TriggerRestartLevel);
		Messenger.RemoveListener ("Touch.Right", TriggerNextLevel);
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (InputExtensions.Pressed.Up)
		{
			TriggerReturnToTitleScreen();
			return;
		}
		
		if (InputExtensions.Pressed.Left)
		{
			TriggerRestartLevel();
			return;
		}
		
		if (InputExtensions.Pressed.Right ||
		    InputExtensions.Pressed.A ||
		    InputExtensions.Pressed.Start) {
			TriggerNextLevel();
			return;
		}
		
	}
	
	void TriggerNextLevel() {
		Disable ();
		TriggerFade(NextLevel, new Color(1, 1, 1, 0));
		AudioHandler.Play("selection");
		ArrowFeedback.Right();
	}
	
	void NextLevel() {
		GameState.LoadNextLevel();
	}
	
	void TriggerRestartLevel() {
		Disable ();
		TriggerFade(RestartLevel, new Color(1, 1, 1, 0));
		AudioHandler.Play("selection");
		ArrowFeedback.Left();
	}
	
	void RestartLevel() {
		GameState.LoadLevel();
	}
	
	void TriggerReturnToTitleScreen() {
		Disable ();
		TriggerFade(ReturnToTitleScreen, new Color(0, 0, 0, 0));
		AudioHandler.Play("selection");
		ArrowFeedback.Up();
	}
	
	void ReturnToTitleScreen() {
		Application.LoadLevel("TitleScreen");
	}
	
}

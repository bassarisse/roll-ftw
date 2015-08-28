using UnityEngine;
using System.Collections;

public class LevelSelectControl : BaseControl {
	
	// Use this for initialization
	void Start () {
		
		AudioHandler.Load ("selection");
		AudioHandler.Load ("cursor");

		GameState.CurrentLevel = GameState.MaxReachedLevel;
		
		Messenger.AddListener ("Touch.Up", ChangeLevelUp);
		Messenger.AddListener ("Touch.Down", ChangeLevelDown);
		Messenger.AddListener ("Touch.Left", TriggerGameStart);
		Messenger.AddListener ("Touch.Right", TriggerReturnToTitleScreen);
		
	}
	
	void Disable() {
		
		this.enabled = false;

		Messenger.RemoveListener ("Touch.Up", ChangeLevelUp);
		Messenger.RemoveListener ("Touch.Down", ChangeLevelDown);
		Messenger.RemoveListener ("Touch.Left", TriggerGameStart);
		Messenger.RemoveListener ("Touch.Right", TriggerReturnToTitleScreen);
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (InputExtensions.Pressed.Up)
			ChangeLevelUp ();
		
		if (InputExtensions.Pressed.Down)
			ChangeLevelDown ();
		
		if (InputExtensions.Pressed.Left ||
		    InputExtensions.Pressed.A ||
		    InputExtensions.Pressed.Start) {
			TriggerGameStart();
			return;
		}
		
		if (InputExtensions.Pressed.Right ||
		    InputExtensions.Pressed.B) {
			TriggerReturnToTitleScreen();
			return;
		}
		
	}
	
	void ChangeLevelUp() {
		GameState.CurrentLevel++;
		if (GameState.CurrentLevel > GameState.MaxReachedLevel)
			GameState.CurrentLevel = GameState.MaxReachedLevel;
		else
			AudioHandler.Play("cursor");
		ArrowFeedback.Up();
	}
	
	void ChangeLevelDown() {
		GameState.CurrentLevel--;
		if (GameState.CurrentLevel < 1)
			GameState.CurrentLevel = 1;
		else
			AudioHandler.Play("cursor");
		ArrowFeedback.Down();
	}
	
	void TriggerGameStart() {
		Disable ();
		TriggerFade(GameStart, new Color(1, 1, 1, 0));
		AudioHandler.Play("selection");
		ArrowFeedback.Left();
	}
	
	void GameStart() {
		GameState.LoadLevel ();
	}
	
	void TriggerReturnToTitleScreen() {
		Disable ();
		TriggerFade(ReturnToTitleScreen, new Color(0, 0, 0, 0));
		AudioHandler.Play("selection");
		ArrowFeedback.Right();
	}
	
	void ReturnToTitleScreen() {
		Application.LoadLevel ("TitleScreen");
	}
	
}

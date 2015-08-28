using UnityEngine;
using System.Collections;

public class HelpControl : BaseControl {
	
	// Use this for initialization
	void Start () {
		
		AudioHandler.Load ("selection");
		
		Messenger.AddListener ("Touch.Left", TriggerReturnToTitleScreen);
		Messenger.AddListener ("Touch.Right", TriggerGameStart);
		
	}
	
	void Disable() {
		
		this.enabled = false;
		
		Messenger.RemoveListener ("Touch.Left", TriggerReturnToTitleScreen);
		Messenger.RemoveListener ("Touch.Right", TriggerGameStart);
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (InputExtensions.Pressed.Right ||
		    InputExtensions.Pressed.A ||
		    InputExtensions.Pressed.Start) {
			TriggerGameStart();
			return;
		}

		if (InputExtensions.Pressed.Left ||
		    InputExtensions.Pressed.B) {
			TriggerReturnToTitleScreen();
			return;
		}
		
	}
	
	void TriggerGameStart() {
		Disable ();
		TriggerFade(GameStart, new Color(1, 1, 1, 0));
		AudioHandler.Play("selection");
		ArrowFeedback.Right();
	}
	
	void GameStart() {
		GameState.LoadLevel (1);
	}
	
	void TriggerReturnToTitleScreen() {
		Disable ();
		TriggerFade(ReturnToTitleScreen, new Color(0, 0, 0, 0));
		AudioHandler.Play("selection");
		ArrowFeedback.Left();
	}
	
	void ReturnToTitleScreen() {
		Application.LoadLevel ("TitleScreen");
	}
	
}

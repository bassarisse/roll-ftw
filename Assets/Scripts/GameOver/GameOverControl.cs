using UnityEngine;
using System.Collections;

public class GameOverControl : BaseControl {

	bool _firstUpdate;

	// Use this for initialization
	void Start () {

		_firstUpdate = true;
		
		AudioHandler.Load ("selection");
		AudioHandler.Load ("win2");
		
		Messenger.AddListener ("Touch.Up", TriggerExit);
		Messenger.AddListener ("Touch.Right", TriggerExit);

	}
	
	void Disable() {
		
		this.enabled = false;

		Messenger.RemoveListener ("Touch.Up", TriggerExit);
		Messenger.RemoveListener ("Touch.Right", TriggerExit);
		
	}
	
	// Update is called once per frame
	void Update () {

		if (_firstUpdate) {
			_firstUpdate = false;
			AudioHandler.Play("win2", 0.65f);
		}
		
		if (InputExtensions.Pressed.Right ||
		    InputExtensions.Pressed.Up ||
		    InputExtensions.Pressed.A ||
		    InputExtensions.Pressed.B ||
		    InputExtensions.Pressed.Start) {
			TriggerExit();
			return;
		}

	}
	
	void TriggerExit() {
		Disable ();
		TriggerFade(Exit, new Color(0, 0, 0, 0));
		AudioHandler.Play("selection");
		ArrowFeedback.Right();
	}

	void Exit() {
		Application.LoadLevel("SplashScreen");
	}

}

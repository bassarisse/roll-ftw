using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AboutControl : BaseControl {
	
	// Use this for initialization
	void Start () {
		
		AudioHandler.Load ("selection");

		Messenger.AddListener ("Touch.Down", TriggerReturnToTitleScreen);
		
	}

	void Disable() {

		this.enabled = false;

		Messenger.RemoveListener ("Touch.Down", TriggerReturnToTitleScreen);

	}
	
	// Update is called once per frame
	void Update () {
		
		if (InputExtensions.Pressed.Down ||
		    InputExtensions.Pressed.A ||
		    InputExtensions.Pressed.B ||
		    InputExtensions.Pressed.Start) {
			TriggerReturnToTitleScreen();
			return;
		}
		
	}
	
	void TriggerReturnToTitleScreen() {
		Disable();
		TriggerFade(ReturnToTitleScreen, new Color(0, 0, 0, 0));
		AudioHandler.Play("selection");
		ArrowFeedback.Down();
	}
	
	void ReturnToTitleScreen() {
		SceneManager.LoadScene ("TitleScreen");
	}
	
}
using UnityEngine;
using System.Collections;

public class AboutControl : MonoBehaviour {
	
	public Fader fader;
	
	// Use this for initialization
	void Start () {
		
		AudioHandler.Load ("selection");
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (InputExtensions.Pressed.Down ||
		    InputExtensions.Pressed.A ||
		    InputExtensions.Pressed.B ||
		    InputExtensions.Pressed.Start) {
			if (fader == null) {
				ReturnToTitleScreen();
			} else {
				fader.SetColor(new Color(0, 0, 0, 0));
				fader.Play(true, gameObject, "ReturnToTitleScreen");
			}
			AudioHandler.Play("selection");
			ArrowFeedback.Down();
			this.enabled = false;
			return;
		}
		
	}
	
	void ReturnToTitleScreen() {
		Application.LoadLevel ("TitleScreen");
	}
	
}

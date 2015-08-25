using UnityEngine;
using System.Collections;

public class TitleScreenControl : MonoBehaviour {

	public Fader fader;

	// Use this for initialization
	void Start () {

		AudioHandler.Load ("selection");

	}
	
	// Update is called once per frame
	void Update () {

		if (GameState.MaxReachedLevel > 1 && InputExtensions.Pressed.Left) {
			if (fader == null) {
				LevelSelect();
			} else {
				fader.SetColor(new Color(0, 0, 0, 0));
				fader.Play(true, gameObject, "LevelSelect");
			}
			AudioHandler.Play("selection");
			ArrowFeedback.Left();
			this.enabled = false;
			return;
		}
		
		if (InputExtensions.Pressed.Up) {
			if (fader == null) {
				OpenAbout();
			} else {
				fader.SetColor(new Color(0, 0, 0, 0));
				fader.Play(true, gameObject, "OpenAbout");
			}
			AudioHandler.Play("selection");
			ArrowFeedback.Up();
			this.enabled = false;
			return;
		}
		
		if (InputExtensions.Pressed.Right ||
		    InputExtensions.Pressed.A ||
		    InputExtensions.Pressed.Start) {
			if (fader == null) {
				StartGame();
			} else {
				fader.SetColor(new Color(0, 0, 0, 0));
				fader.Play(true, gameObject, "StartGame");
			}
			AudioHandler.Play("selection");
			ArrowFeedback.Right();
			this.enabled = false;
			return;
		}

	}
	
	void StartGame() {
		Application.LoadLevel ("Help");
	}
	
	void OpenAbout() {
		Application.LoadLevel ("About");
	}
	
	void LevelSelect() {
		Application.LoadLevel ("LevelSelect");
	}

}

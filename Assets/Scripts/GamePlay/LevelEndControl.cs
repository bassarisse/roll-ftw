using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelEndControl : MonoBehaviour {
	
	public Fader fader;

	void Start() {
		
		AudioHandler.Load ("selection");

	}
	
	// Update is called once per frame
	void Update () {
		
		if (InputExtensions.Pressed.Up)
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
		
		if (InputExtensions.Pressed.Left)
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
		
		if (InputExtensions.Pressed.Right ||
		    InputExtensions.Pressed.A ||
		    InputExtensions.Pressed.Start) {
			if (fader == null) {
				NextLevel();
			} else {
				fader.SetColor(new Color(1, 1, 1, 0));
				fader.Play(true, gameObject, "NextLevel");
			}
			AudioHandler.Play("selection");
			this.enabled = false;
			return;
		}
		
	}
	
	void NextLevel() {
		GameState.LoadNextLevel();
	}
	
	void RestartLevel() {
		GameState.LoadLevel();
	}
	
	void ReturnToTitleScreen() {
		Application.LoadLevel("TitleScreen");
	}
	
}

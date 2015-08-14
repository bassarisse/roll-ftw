using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelEndControl : MonoBehaviour {
	
	public Fader fader;
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			if (fader == null) {
				ReturnToTitleScreen();
			} else {
				fader.SetColor(new Color(0, 0, 0, 0));
				fader.Play(true, gameObject, "ReturnToTitleScreen");
			}
			this.enabled = false;
			return;
		}
		
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			if (fader == null) {
				RepeatLevel();
			} else {
				fader.SetColor(new Color(1, 1, 1, 0));
				fader.Play(true, gameObject, "RepeatLevel");
			}
			this.enabled = false;
			return;
		}
		
		if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Return))
		{
			if (fader == null) {
				NextLevel();
			} else {
				fader.SetColor(new Color(1, 1, 1, 0));
				fader.Play(true, gameObject, "NextLevel");
			}
			this.enabled = false;
			return;
		}
		
	}
	
	void NextLevel() {
		GameState.LoadNextLevel();
	}
	
	void RepeatLevel() {
		GameState.LoadLevel();
	}
	
	void ReturnToTitleScreen() {
		Application.LoadLevel("TitleScreen");
	}
	
}

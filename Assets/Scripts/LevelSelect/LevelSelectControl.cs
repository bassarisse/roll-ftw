using UnityEngine;
using System.Collections;

public class LevelSelectControl : MonoBehaviour {
	
	public Fader fader;
	
	// Use this for initialization
	void Start () {
		
		AudioHandler.Load ("selection");
		AudioHandler.Load ("cursor");

		GameState.CurrentLevel = GameState.MaxReachedLevel;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (InputExtensions.Pressed.Up) {
			GameState.CurrentLevel++;
			if (GameState.CurrentLevel > GameState.MaxReachedLevel)
				GameState.CurrentLevel = GameState.MaxReachedLevel;
			else
				AudioHandler.Play("cursor");
			ArrowFeedback.Up();
		}
		
		if (InputExtensions.Pressed.Down) {
			GameState.CurrentLevel--;
			if (GameState.CurrentLevel < 1)
				GameState.CurrentLevel = 1;
			else
				AudioHandler.Play("cursor");
			ArrowFeedback.Down();
		}
		
		if (InputExtensions.Pressed.Left ||
		    InputExtensions.Pressed.A ||
		    InputExtensions.Pressed.Start) {
			if (fader == null) {
				StartGame();
			} else {
				fader.SetColor(new Color(1, 1, 1, 0));
				fader.Play(true, gameObject, "StartGame");
			}
			AudioHandler.Play("selection");
			ArrowFeedback.Left();
			this.enabled = false;
			return;
		}
		
		if (InputExtensions.Pressed.Right ||
		    InputExtensions.Pressed.B) {
			if (fader == null) {
				ReturnToTitleScreen();
			} else {
				fader.SetColor(new Color(0, 0, 0, 0));
				fader.Play(true, gameObject, "ReturnToTitleScreen");
			}
			AudioHandler.Play("selection");
			ArrowFeedback.Right();
			this.enabled = false;
			return;
		}
		
	}
	
	void StartGame() {
		GameState.LoadLevel ();
	}
	
	void ReturnToTitleScreen() {
		Application.LoadLevel ("TitleScreen");
	}
	
}

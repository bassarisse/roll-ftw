using UnityEngine;
using System.Collections;

public class HelpControl : MonoBehaviour {
	
	public Fader fader;
	
	// Use this for initialization
	void Start () {
		
		AudioHandler.Load ("selection");
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.RightArrow) ||
		    Input.GetKeyDown (KeyCode.Space) ||
		    Input.GetKeyDown (KeyCode.Return)) {
			if (fader == null) {
				StartGame();
			} else {
				fader.SetColor(new Color(1, 1, 1, 0));
				fader.Play(true, gameObject, "StartGame");
			}
			AudioHandler.Play("selection");
			this.enabled = false;
			return;
		}
		
		if (Input.GetKeyDown (KeyCode.LeftArrow) ||
		    Input.GetKeyDown (KeyCode.Escape)) {
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
		
	}
	
	void StartGame() {
		GameState.LoadLevel (1);
	}
	
	void ReturnToTitleScreen() {
		Application.LoadLevel ("TitleScreen");
	}
	
}

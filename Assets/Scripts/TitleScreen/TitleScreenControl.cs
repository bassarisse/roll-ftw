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

		if (GameState.MaxReachedLevel > 1 && Input.GetKeyDown (KeyCode.LeftArrow)) {
			if (fader == null) {
				LevelSelect();
			} else {
				fader.SetColor(new Color(0, 0, 0, 0));
				fader.Play(true, gameObject, "LevelSelect");
			}
			AudioHandler.Play("selection");
			this.enabled = false;
			return;
		}
		
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			if (fader == null) {
				OpenAbout();
			} else {
				fader.SetColor(new Color(0, 0, 0, 0));
				fader.Play(true, gameObject, "OpenAbout");
			}
			AudioHandler.Play("selection");
			this.enabled = false;
			return;
		}
		
		if (Input.GetKeyDown (KeyCode.RightArrow) ||
		    Input.GetKeyDown (KeyCode.Space) ||
		    Input.GetKeyDown (KeyCode.Return)) {
			if (fader == null) {
				StartGame();
			} else {
				fader.SetColor(new Color(0, 0, 0, 0));
				fader.Play(true, gameObject, "StartGame");
			}
			AudioHandler.Play("selection");
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

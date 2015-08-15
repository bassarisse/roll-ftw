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

		if (GameState.MaxReachedLevel > 1 && Input.GetKey (KeyCode.LeftArrow)) {
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
		
		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return)) {
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

	}
	
	void StartGame() {
		GameState.LoadLevel (1);
	}
	
	void LevelSelect() {
		Application.LoadLevel ("LevelSelect");
	}

}

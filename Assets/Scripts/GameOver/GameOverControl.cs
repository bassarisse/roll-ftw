using UnityEngine;
using System.Collections;

public class GameOverControl : MonoBehaviour {

	public Fader fader;

	bool _firstUpdate;

	// Use this for initialization
	void Start () {

		_firstUpdate = true;
		
		AudioHandler.Load ("selection");
		AudioHandler.Load ("win2");

	}
	
	// Update is called once per frame
	void Update () {

		if (_firstUpdate) {
			_firstUpdate = false;
			AudioHandler.Play("win2", 0.65f);
		}
		
		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.UpArrow) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Return)) {
			if (fader == null) {
				Exit();
			} else {
				fader.SetColor(new Color(0, 0, 0, 0));
				fader.Play(true, gameObject, "Exit");
			}
			this.enabled = false;
			AudioHandler.Play("selection");
			return;
		}

	}

	void Exit() {
		Application.LoadLevel("SplashScreen");
	}

}

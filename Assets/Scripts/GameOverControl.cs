using UnityEngine;
using System.Collections;

public class GameOverControl : MonoBehaviour {

	public Fader fader;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.UpArrow) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Return)) {
			if (fader == null) {
				Exit();
			} else {
				fader.SetColor(new Color(0, 0, 0, 0));
				fader.Play(true, gameObject, "Exit");
			}
			this.enabled = false;
		}

	}

	void Exit() {
		Application.LoadLevel("SplashScreen");
	}

}

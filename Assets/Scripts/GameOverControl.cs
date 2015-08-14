using UnityEngine;
using System.Collections;

public class GameOverControl : MonoBehaviour {

	public Fader fader;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (KeyCode.RightArrow)) {
			this.enabled = false;
			if (fader == null) {
				Exit();
			} else {
				fader.SetColor(new Color(0, 0, 0, 0));
				fader.Play(true, gameObject, "Exit");
			}
		}
	}

	void Exit() {
		Application.LoadLevel("SplashScreen");
	}

}

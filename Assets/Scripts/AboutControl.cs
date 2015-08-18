using UnityEngine;
using System.Collections;

public class AboutControl : MonoBehaviour {
	
	public Fader fader;
	
	// Use this for initialization
	void Start () {
		
		AudioHandler.Load ("selection");
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.DownArrow) ||
		    Input.GetKeyDown (KeyCode.Escape) ||
		    Input.GetKeyDown (KeyCode.Space) ||
		    Input.GetKeyDown (KeyCode.Return)) {
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
	
	void ReturnToTitleScreen() {
		Application.LoadLevel ("TitleScreen");
	}
	
}

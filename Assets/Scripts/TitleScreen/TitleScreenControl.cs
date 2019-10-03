using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleScreenControl : BaseControl {

	// Use this for initialization
	void Start () {

		AudioHandler.Load ("selection");
		
		Messenger.AddListener ("Touch.Left", TriggerLevelSelect);
		Messenger.AddListener ("Touch.Up", TriggerAbout);
		Messenger.AddListener ("Touch.Right", TriggerGameStart);

	}
	
	void Disable() {

		this.enabled = false;

		Messenger.RemoveListener ("Touch.Left", TriggerLevelSelect);
		Messenger.RemoveListener ("Touch.Up", TriggerAbout);
		Messenger.RemoveListener ("Touch.Right", TriggerGameStart);

	}
	
	// Update is called once per frame
	void Update () {

		if (InputExtensions.Pressed.Left) {
			TriggerLevelSelect();
			return;
		}
		
		if (InputExtensions.Pressed.Up) {
			TriggerAbout();
			return;
		}
		
		if (InputExtensions.Pressed.Right ||
		    InputExtensions.Pressed.A ||
		    InputExtensions.Pressed.Start) {
			TriggerGameStart();
			return;
		}

	}
	
	void TriggerGameStart()
    {
        Debug.LogWarning("Title Start");
		Disable ();
		TriggerFade (GameStart, new Color(0, 0, 0, 0));
		AudioHandler.Play("selection");
		ArrowFeedback.Right();
	}
	
	void GameStart() {
		SceneManager.LoadScene ("Help");
	}
	
	void TriggerAbout() {
		Disable ();
		TriggerFade (About, new Color(0, 0, 0, 0));
		AudioHandler.Play("selection");
		ArrowFeedback.Up();
	}
	
	void About() {
		SceneManager.LoadScene ("About");
	}
	
	void TriggerLevelSelect() {
		if (GameState.MaxReachedLevel <= 1)
			return;
		Disable ();
		TriggerFade (LevelSelect, new Color(0, 0, 0, 0));
		AudioHandler.Play("selection");
		ArrowFeedback.Left();
	}
	
	void LevelSelect() {
		SceneManager.LoadScene ("LevelSelect");
	}

}

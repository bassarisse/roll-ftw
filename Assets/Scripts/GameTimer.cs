using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameTimer : MonoBehaviour {
	
	static bool running;
	static float time;

	Text _text;

	// Use this for initialization
	void Start () {
		_text = GetComponent<Text> ();
	}

	void Awake() {
		GameTimer.running = true;
		GameTimer.time = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (GameTimer.running)
			GameTimer.time += Time.deltaTime;

		var time = GameTimer.time;

		var seconds = Mathf.Floor (time % 60.0f);
		var minutes = Mathf.Floor (time / 60.0f);
		var hours = Mathf.Floor (time / 3600.0f);
		var miliseconds = Mathf.Floor((time - Mathf.Floor(time)) * 1000.0f);

		_text.text = string.Format("{0:#0}:{1:00}:{2:00}.{3:000}", hours, minutes, seconds, miliseconds );
		
	}
}

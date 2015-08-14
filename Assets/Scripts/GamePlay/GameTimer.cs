using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

[RequireComponent(typeof(Text))]
public class GameTimer : MonoBehaviour {
	
	public static bool running;
	public static float time;

	Text _text;
	float _startTime;

	// Use this for initialization
	void Start () {

		_text = GetComponent<Text> ();
		_text.enabled = false;
		_startTime = 0f;

	}

	void Awake() {

		GameTimer.running = true;
		GameTimer.time = 0f;

	}
	
	// Update is called once per frame
	void Update () {

		if (_startTime < 0.5f) {
			_startTime += Time.deltaTime;
			return;
		}
		
		if (GameTimer.running)
			GameTimer.time += Time.deltaTime;

		var time = GameTimer.time;

		if (time < 0f)
			time = 0f;

		var seconds = Mathf.Floor (time % 60.0f);
		var minutes = Mathf.Floor (time / 60.0f);
		var hours = Mathf.Floor (time / 3600.0f);
		var miliseconds = Mathf.Floor((time - Mathf.Floor(time)) * 1000.0f);
		
		_text.enabled = true;
		_text.text = string.Format("{0:#0}:{1:00}:{2:00}.{3:000}", hours, minutes, seconds, miliseconds );
		
	}
}

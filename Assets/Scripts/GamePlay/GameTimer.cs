using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

[RequireComponent(typeof(Text))]
public class GameTimer : MonoBehaviour {
	
	public static bool running;
	public static float time;

	public static string FormatTime(float time) {

		if (time < 0f)
			time = 0f;
		
		var seconds = Mathf.Floor (time % 60f);
		var minutes = Mathf.Floor (time / 60f);
		var hours = Mathf.Floor (time / 3600f);
		var miliseconds = Mathf.Floor((time - Mathf.Floor(time)) * 1000f);

		minutes -= hours * 60f;
		
		var format = "{1:00}:{2:00}.{3:000}";
		
		if (hours > 0f)
			format = "{0:#0}:" + format;
		
		return string.Format (format, hours, minutes, seconds, miliseconds);

	}

	Text _text;
	float _startTimer;
	float _startDelayTime = 0.5f;

	// Use this for initialization
	void Start () {

		_text = GetComponent<Text> ();
		_text.enabled = false;
		_startTimer = 0f;

		Messenger.AddListener ("LevelPause", Deactivate);
		Messenger.AddListener ("LevelResume", Activate);

	}
	
	void Deactivate() {
		this.enabled = false;
	}
	
	void Activate() {
		this.enabled = true;
	}

	void Awake() {

		GameTimer.running = true;
		GameTimer.time = 0f;

	}
	
	// Update is called once per frame
	void Update () {

		if (_startTimer < _startDelayTime) {
			_startTimer += Time.deltaTime;
			if (_startTimer >= _startDelayTime)
				Messenger.Broadcast("LevelStart");
			return;
		}
		
		if (GameTimer.running)
			GameTimer.time += Time.deltaTime;
		
		_text.enabled = true;
		_text.text = FormatTime(GameTimer.time);
		
	}
}

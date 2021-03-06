﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioHandlerHelper : MonoBehaviour {
	
	private IDictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

	void Start() {
		//gameObject.AddComponent<AudioSource> ();
	}
	
	void Awake () {
		DontDestroyOnLoad(gameObject);	
	}

	public void OnLevelWasLoaded(int unused) {
		Cleanup ();
	}
	
	public bool Load(string audioName) {
		
		if (_audioClips.ContainsKey (audioName))
			return true;
		
		var audioClip = Resources.Load ("Audio/" + audioName) as AudioClip;
		if (audioClip == null)
			return false;
		
		_audioClips.Add (audioName, audioClip);
		
		return true;
		
	}
	
	public bool Play(string audioName, float volume = 1.0f) {
		return Play (audioName, Vector3.zero, volume);
	}
	
	public bool Play(string audioName, Vector3 point, float volume = 1.0f) {
		
		if (!_audioClips.ContainsKey (audioName)) {
			if (!Load(audioName))
				return false;
		}
		
		var audioClip = _audioClips[audioName];
		if (audioClip == null)
			return false;
		
		AudioSource.PlayClipAtPoint (audioClip, point, volume);
		
		return true;
	}
	
	public void Cleanup() {
		foreach (var item in _audioClips) {
			Resources.UnloadAsset (item.Value);
		}
		_audioClips.Clear ();
	}
}

static internal class AudioHandler {
	
	//Disable the unused variable warning
	#pragma warning disable 0414
	static private AudioHandlerHelper _audioHandlerHelper = ( new GameObject("AudioHandlerHelper") ).AddComponent<AudioHandlerHelper>();
	#pragma warning restore 0414

	static public bool Load(string audioName) {
		return _audioHandlerHelper.Load (audioName);
	}
	
	static public bool Play(string audioName, float volume = 1.0f) {
		return _audioHandlerHelper.Play (audioName, volume);
	}
	
	static public bool Play(string audioName, Vector3 point, float volume = 1.0f) {
		return _audioHandlerHelper.Play (audioName, point, volume);
	}
	
	static public void Cleanup() {
		_audioHandlerHelper.Cleanup ();
	}

}
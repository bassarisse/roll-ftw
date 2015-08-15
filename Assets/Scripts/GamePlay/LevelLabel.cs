using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LevelLabel : MonoBehaviour {
	
	Text _text;

	// Use this for initialization
	void Start () {

		_text = GetComponent<Text> ();
		_text.text = "LEVEL " + GameState.CurrentLevel.ToString ();
		_text.enabled = false;

		Messenger.AddListener ("LevelStart", Show);

	}

	void Show() {
		_text.enabled = true;
	}
}

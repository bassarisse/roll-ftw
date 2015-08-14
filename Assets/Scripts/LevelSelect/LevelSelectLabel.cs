using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LevelSelectLabel : MonoBehaviour {

	Text _text;

	// Use this for initialization
	void Start () {
		_text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		_text.text = GameState.CurrentLevel.ToString ();
	}
}

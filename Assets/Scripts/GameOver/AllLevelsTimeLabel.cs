using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class AllLevelsTimeLabel : MonoBehaviour {
	
	Text _text;
	
	// Use this for initialization
	void Start () {
		_text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		var totalTime = GameState.GetAllLevelsTime ();
		_text.text = totalTime > 0f ? GameTimer.FormatTime (totalTime) : "-";
	}
}

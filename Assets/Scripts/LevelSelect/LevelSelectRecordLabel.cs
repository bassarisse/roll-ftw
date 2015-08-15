using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LevelSelectRecordLabel : MonoBehaviour {
	
	Text _text;

	// Use this for initialization
	void Start () {
		_text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		var recordTime = GameState.GetLevelTime (GameState.CurrentLevel);
		_text.text = recordTime > 0f ? GameTimer.FormatTime (recordTime) : "-";
	}
}

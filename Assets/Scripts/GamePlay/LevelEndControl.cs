using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelEndControl : MonoBehaviour {
	
	public bool EnableControl = false;
	
	// Use this for initialization
	void Start () {
		
		Messenger.AddListener ("LevelEnd", SetEnableControl);
		
	}
	
	void SetEnableControl() {
		this.EnableControl = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!this.EnableControl)
			return;
		
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			GameState.LoadLevel();
			this.EnableControl = false;
			return;
		}
		
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			GameState.LoadNextLevel();
			this.EnableControl = false;
			return;
		}
		
	}
	
}

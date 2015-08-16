using UnityEngine;
using System.Collections;

public class EnableOnPause : MonoBehaviour {
	
	public MonoBehaviour target;
	
	void Start () {
		
		if (target == null)
			return;
		
		target.enabled = false;
		
		Messenger.AddListener("LevelPause", Show);
		Messenger.AddListener("LevelResume", Hide);
		
	}
	
	void Show() {

		target.enabled = true;
		
	}
	
	void Hide() {

		target.enabled = false;
		
	}

}

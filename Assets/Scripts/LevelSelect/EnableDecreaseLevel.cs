using UnityEngine;
using System.Collections;

public class EnableDecreaseLevel : MonoBehaviour {
	
	public MonoBehaviour target;
	
	void Start () {
		UpdateVisibility ();
	}
	
	void Update() {
		UpdateVisibility ();
	}
	
	void UpdateVisibility() {
		if (target != null)
			target.enabled = GameState.CurrentLevel > 1;
	}
}

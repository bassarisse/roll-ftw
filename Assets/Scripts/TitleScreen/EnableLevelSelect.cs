using UnityEngine;
using System.Collections;

public class EnableLevelSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {

		if (GameState.MaxReachedLevel <= 1)
			Destroy (gameObject);
	
	}
}

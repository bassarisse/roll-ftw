using UnityEngine;
using System.Collections;

public class SplashExit : MonoBehaviour {

	void Start() {
		FullScreenLocker.ControlEnabled = true;
	}

	void EndSplash () {

		Application.LoadLevel ("TitleScreen");
	
	}
}

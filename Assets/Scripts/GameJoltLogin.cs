using UnityEngine;
using System.Collections;

public class GameJoltLogin : MonoBehaviour {
	
	// Use this for initialization
	void Start () {

#if UNITY_WEBPLAYER || UNITY_WEBGL || UNITY_ANDROID
		Next();
#else
		var isSignedIn = GameJolt.API.Manager.Instance.CurrentUser != null;
		if (isSignedIn)
			Next();
		else
			GameJolt.UI.Manager.Instance.ShowSignIn(success => Next());
#endif

	}

	void Next() {
		
		Application.LoadLevel("SplashScreen");

	}

}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameJoltLogin : MonoBehaviour {
	
	// Use this for initialization
	void Start () {

		var wait = 1;

#if UNITY_IOS
		wait = 4;
#endif
		
#if UNITY_WEBPLAYER || UNITY_WEBGL || UNITY_ANDROID || UNITY_IOS
		StartCoroutine(GoOn(wait));
#else
		var isSignedIn = GameJolt.API.Manager.Instance.CurrentUser != null;
		if (isSignedIn)
			Next();
		else
			GameJolt.UI.Manager.Instance.ShowSignIn(success => Next());
#endif

	}

	void Next() {
		SceneManager.LoadScene ("GameSplashScreen");
	}

	IEnumerator GoOn(int wait) {
		yield return new WaitForSeconds(wait);
		Next();
	}

}

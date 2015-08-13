using UnityEngine;
using System.Collections;

public static class GameState {
	
	public const int MaxLevel = 2;
	public static int CurrentLevel = 1;
	
	public static void Reset () {
		CurrentLevel = 1;
	}
	
	public static void LoadNextLevel() {
		CurrentLevel++;
		LoadLevel ();
	}
	
	public static void LoadLevel(int level) {
		CurrentLevel = level;
		
		if (CurrentLevel < 1)
			CurrentLevel = 1;
		
		if (CurrentLevel > MaxLevel)
			CurrentLevel = MaxLevel;

		LoadLevel ();
	}
	
	public static void LoadLevel() {

		if (CurrentLevel > MaxLevel) {
			Reset();
			Application.LoadLevel ("SplashScreen");
			return;
		}

		Application.LoadLevel ("Game" + CurrentLevel.ToString ());

	}

}
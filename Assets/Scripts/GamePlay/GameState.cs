using UnityEngine;
using System.Collections;

public static class GameState {
	
	const string MAX_REACHED_LEVEL_KEY = "maxReachedLevel";
	const string LEVEL_SCORE_KEY = "levelScore";
	
	public static int MaxReachedLevel {
		get {
			return PlayerPrefs.GetInt(MAX_REACHED_LEVEL_KEY, 1);
		}
		set {
			PlayerPrefs.SetInt(MAX_REACHED_LEVEL_KEY, value);
			PlayerPrefs.Save();
		}
	}
	
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

		if (CurrentLevel > MaxReachedLevel)
			MaxReachedLevel = CurrentLevel;
		
		Application.LoadLevel ("Game" + CurrentLevel.ToString ());
		
	}
	
	private static string GetLevelScoreKey(int level) {
		return LEVEL_SCORE_KEY + level.ToString();
	}
	
	public static void SaveLevelTime(float time) {
		SaveLevelTime (CurrentLevel, time);
	}
	
	public static void SaveLevelTime(int level, float time) {
		PlayerPrefs.SetFloat (GetLevelScoreKey (level), time);
		PlayerPrefs.Save ();
	}
	
	public static bool IsRecord(float time) {
		return IsRecord (CurrentLevel, time);
	}
	
	public static bool IsRecord(int level, float time) {
		
		if (time <= 0f)
			return false;

		var isRecord = false;
		var levelTime = PlayerPrefs.GetFloat (GetLevelScoreKey (level), -1f);

		if (levelTime < 0f)
			isRecord = true;
		else
			isRecord = time < levelTime;

		if (isRecord)
			SaveLevelTime (level, time);

		return isRecord;
	}

}
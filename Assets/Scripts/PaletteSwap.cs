using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PaletteSwap : MonoBehaviour {

	const string PALETTE_INDEX_KEY = "paletteIndex";
	
	public static int PaletteIndex {
		get {
			return PlayerPrefs.GetInt(PALETTE_INDEX_KEY, 0);
		}
		set {
			PlayerPrefs.SetInt(PALETTE_INDEX_KEY, value);
			PlayerPrefs.Save();
		}
	}
	
	public bool EnableSwap = false;
	public bool EnableUp = false;
	public bool EnableDown = true;

	Resolutioner _resolutioner;
	Texture2D[] _palettes;

	// Use this for initialization
	void Start () {

#if UNITY_EDITOR
		ResetPalette();
#endif
		
		AudioHandler.Load("palette_change");

		_resolutioner = GetComponent<Resolutioner> ();

		_palettes = Resources.LoadAll<Texture2D>("ProcessedPalettes");

		UpdatePalette ();

		Messenger.AddListener<bool> ("EnablePalleteSwap", SetEnableSwap);
		Messenger.AddListener ("Touch.Up", ChangePaletteUp);
		Messenger.AddListener ("Touch.Down", ChangePaletteDown);
	
	}
	
	void SetEnableSwap(bool enable) {
		this.EnableSwap = enabled;
	}

	// Update is called once per frame
	void Update () {
		
		if (InputExtensions.Pressed.Up)
			ChangePaletteUp();
		
		if (InputExtensions.Pressed.Down)
			ChangePaletteDown();
	
	}
	
	void ChangePaletteUp() {
		if (!EnableSwap || !EnableUp)
			return;
		PaletteSwap.PaletteIndex++;
		if (PaletteSwap.PaletteIndex >= _palettes.Length)
			PaletteSwap.PaletteIndex = 0;
		AudioHandler.Play("palette_change");
		ArrowFeedback.Up();
		UpdatePalette();
	}
	
	void ChangePaletteDown() {
		if (!EnableSwap || !EnableDown)
			return;
		PaletteSwap.PaletteIndex--;
		if (PaletteSwap.PaletteIndex < 0)
			PaletteSwap.PaletteIndex = _palettes.Length - 1;
		AudioHandler.Play("palette_change");
		ArrowFeedback.Down();
		UpdatePalette();
	}

	private void UpdatePalette() {
		if (_resolutioner == null)
			return;
		_resolutioner.postprocessColor.SetPalette(_palettes [PaletteSwap.PaletteIndex]);
	}
	
#if UNITY_EDITOR

	void OnApplicationQuit() {
		ResetPalette ();
	}

	void ResetPalette() {
		PaletteIndex = 0;
		UpdatePalette ();
	}

#endif

}

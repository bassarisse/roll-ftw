using UnityEngine;
using System.Collections;

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
		
		AudioHandler.Load("palette_change");

		_resolutioner = GetComponent<Resolutioner> ();

		_palettes = Resources.LoadAll<Texture2D>("ProcessedPalettes");

		UpdatePalette ();

		Messenger.AddListener<bool> ("EnablePalleteSwap", SetEnableSwap);
	
	}
	
	void SetEnableSwap(bool enable) {
		this.EnableSwap = enabled;
	}

	// Update is called once per frame
	void Update () {

		if (!this.EnableSwap)
			return;
		
		if (EnableUp && Input.GetKeyDown(KeyCode.UpArrow))
		{
			PaletteSwap.PaletteIndex++;
			if (PaletteSwap.PaletteIndex >= _palettes.Length)
				PaletteSwap.PaletteIndex = 0;
			AudioHandler.Play("palette_change");
			UpdatePalette();
		}
		
		if (EnableDown && Input.GetKeyDown(KeyCode.DownArrow))
		{
			PaletteSwap.PaletteIndex--;
			if (PaletteSwap.PaletteIndex < 0)
				PaletteSwap.PaletteIndex = _palettes.Length - 1;
			AudioHandler.Play("palette_change");
			UpdatePalette();
		}
	
	}

	private void UpdatePalette() {
		_resolutioner.postprocessColor.SetPalette(_palettes [PaletteSwap.PaletteIndex]);
	}

}

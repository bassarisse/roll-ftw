using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class PaletteSwap : MonoBehaviour {
	
	public static int paletteIndex = 0;

	public bool EnableSwap = false;

	Resolutioner _resolutioner;
	Texture2D[] _palettes;

	// Use this for initialization
	void Start () {

		_resolutioner = GetComponent<Resolutioner> ();

		_palettes = Resources.LoadAll<Texture2D>("ProcessedPalettes");

		UpdatePalette ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (!this.EnableSwap)
			return;
		
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			PaletteSwap.paletteIndex++;
			if (PaletteSwap.paletteIndex >= _palettes.Length)
				PaletteSwap.paletteIndex = 0;
			UpdatePalette();
		}
		
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			PaletteSwap.paletteIndex--;
			if (PaletteSwap.paletteIndex < 0)
				PaletteSwap.paletteIndex = _palettes.Length - 1;
			UpdatePalette();
		}
	
	}

	private void UpdatePalette() {
		_resolutioner.postprocessColor.SetPalette(_palettes [PaletteSwap.paletteIndex]);
	}

}

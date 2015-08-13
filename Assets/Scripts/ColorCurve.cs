using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

[RequireComponent(typeof(ColorCorrectionCurves))]
public class ColorCurve : MonoBehaviour {

	float _value = 0.0f;
	public float Value = 0.0f;
	
	ColorCorrectionCurves _colorCorrectionCurves = null;

	// Use this for initialization
	void Start () {

		_colorCorrectionCurves = GetComponent<ColorCorrectionCurves> ();

		Messenger.AddListener<float>("ColorCurve.SetValue", SetValue);

		SetValue (Value);
	
	}

	void Update() {

		if (Value != _value)
			SetValue(Value);

	}

	public void SetValue(float value) {

		_value = value;
		Value = value;
		
		_colorCorrectionCurves.redChannel = AnimationCurve.Linear(0f, value, 1f, 1f);
		_colorCorrectionCurves.greenChannel = AnimationCurve.Linear(0f, value, 1f, 1f);
		_colorCorrectionCurves.blueChannel = AnimationCurve.Linear(0f, value, 1f, 1f);

		_colorCorrectionCurves.UpdateParameters();

	}

}
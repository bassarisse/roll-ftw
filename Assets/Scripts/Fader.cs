using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fader : MonoBehaviour {
	
	public bool AutoPlay = true;
	public float AlphaFrom = 1f;
	public float AlphaTo = 0f;
	public float Time = 1f;
	
	Image _image;
	SpriteRenderer _spriteRenderer;

	// Use this for initialization
	void Start () {
		
		_image = GetComponent<Image> ();
		_spriteRenderer = GetComponent<SpriteRenderer> ();

		if (AutoPlay)
			Play (false);
		
	}
	
	public void Play(GameObject completeTarget = null, string completeCallback = null) {
		Play (completeTarget, completeCallback);
	}
	
	public void Play(bool reverse = false, GameObject completeTarget = null, string completeCallback = null) {

		var hash = iTween.Hash (
			"from", reverse ? AlphaTo : AlphaFrom,
			"to", reverse ? AlphaFrom : AlphaTo,
			"time", Time,
			"onupdate", "ChangeAlpha"
		);

		if (completeTarget != null && !string.IsNullOrEmpty (completeCallback)) {
			hash.Add ("oncompletetarget", completeTarget);
			hash.Add ("oncomplete", completeCallback);
		}
		
		iTween.ValueTo (gameObject, hash);

	}

	void ChangeAlpha (float value) {
		
		if (_image != null)
			_image.color = CreateColorWithAlpha(_image.color, value);
		
		if (_spriteRenderer != null)
			_spriteRenderer.color = CreateColorWithAlpha(_spriteRenderer.color, value);

	}

	public void SetColor (Color color) {
		
		if (_image != null)
			_image.color = color;
		
		if (_spriteRenderer != null)
			_spriteRenderer.color = color;
		
	}

	Color CreateColorWithAlpha(Color color, float alpha) {
		return new Color (color.r, color.g, color.b, alpha);
	}
}

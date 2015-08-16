using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextAutoShadow : MonoBehaviour {

	public Color ShadowColor = Color.black;

	// Use this for initialization
	void Start () {
		
		AddShadow (0, 1);
		AddShadow (0, -1);
		AddShadow (1, 0);
		AddShadow (-1, 0);
	
	}

	void AddShadow(int x, int y) {
		
		var shadow = gameObject.AddComponent<Shadow> ();
		shadow.useGraphicAlpha = true;
		shadow.effectColor = ShadowColor;
		shadow.effectDistance = new Vector2 (x, y);

	}
}

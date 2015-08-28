using UnityEngine;
using System.Collections;

public abstract class BaseControl : MonoBehaviour {
	
	public Fader fader;

	public void TriggerFade(Callback handler, Color? color = null) {

		if (fader == null) {
			handler();
		} else {
			if (color.HasValue)
				fader.SetColor(color.Value);
			fader.Play(true, gameObject, handler.Method.Name);
		}

	}
	
}
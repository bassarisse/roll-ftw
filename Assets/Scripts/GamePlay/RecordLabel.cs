using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RecordLabel : MonoBehaviour {

	public Text Text;
	
	void Start () {
		
		if (Text != null)
			Text.enabled = false;
		
		Messenger.AddListener("LevelEnd", Show);
		
	}
	
	void Show() {
		
		if (GameState.IsNewRecord && Text != null)
			Text.enabled = true;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Text == null || !Text.enabled)
			return;

		var a = Mathf.PingPong (Time.time, 1);

		Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, a < 0.15f ? 0f : 1f);
	
	}
}

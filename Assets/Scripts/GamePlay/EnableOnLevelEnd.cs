using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnableOnLevelEnd : MonoBehaviour {

	public MonoBehaviour target;

	void Start () {

		if (target != null)
			target.enabled = false;

		Messenger.AddListener("LevelEnd", Show);

	}

	void Show() {
		
		if (target != null)
			target.enabled = true;

	}
}

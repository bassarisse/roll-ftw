﻿using UnityEngine;
using System.Collections;

public class EnableOnLevelEnd : MonoBehaviour {

	public MonoBehaviour target;

	void Start () {

		if (target == null)
			return;
			
		target.enabled = false;

		Messenger.AddListener("LevelEnd", Show);

	}

	void Show() {

		target.enabled = true;

	}

}
